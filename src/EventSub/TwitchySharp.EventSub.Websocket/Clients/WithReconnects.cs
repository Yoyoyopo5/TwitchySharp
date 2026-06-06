using TwitchySharp.EventSub.Websocket.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Websocket.Clients;

public static class ListenToEventSubWebsocketClientExtensions
{
    public record EventSubWebsocketReconnectClientError(Exception Exception) : Error("An exception was thrown"); 

    private static Func<CancellationToken, Task<Validation>> Concurrently(this SemaphoreSlim semaphore, Func<Task> func)
        => async ct =>
        {
            try
            {
                await semaphore.WaitAsync(ct);
            }
            catch (TaskCanceledException)
            {
                return new Error("The task was cancelled while acquiring a lock.");
            }
            try
            {
                await func();
                return new Validation();
            }
            finally
            {
                semaphore.Release();
            }
        };

    /// <summary>
    /// Wraps an EventSub Websocket listener to support seamless automatic reconnect message handling.
    /// </summary>
    /// <remarks>
    /// This may call <paramref name="listenToClient"/> multiple times over its lifecycle and
    /// expects an implementation that supports being called multiple times, each starting its own connection.
    /// </remarks>
    /// <param name="listenToClient">
    /// The base client to use.
    /// </param>
    /// <returns>A <see cref="ListenToEventSubWebsocketClient"/> that automatically handles reconnect messages.</returns>
    public static ListenToEventSubWebsocketClient WithReconnects(this ListenToEventSubWebsocketClient listenToClient)
    {
        return async (pipeline, url, wrapperCancellationToken) =>
        {
            SemaphoreSlim semaphore = new(1, 1);
            TaskCompletionSource<Validation> wrapperTask = new();
            await using CancellationTokenRegistration registration = wrapperCancellationToken.Register(() => wrapperTask.TrySetResult(new Validation()));

            CancellationTokenSource? current = null;
            CancellationTokenSource? pending = null;

            Task<Validation> setPending(CancellationTokenSource reconnectCts)
                => semaphore.Concurrently(async () =>
                {
                    if (pending is not null)
                        await pending.CancelAsync();
                    pending = reconnectCts;
                })(reconnectCts.Token);

            async Task startNewClient(CancellationTokenSource clientCts, EventSubWebsocketUrl url)
            {
                try
                {
                    await listenToClient(createReconnectPipelineFor(clientCts), url, clientCts.Token);

                    if (clientCts.IsCancellationRequested)
                        return;

                    await semaphore.Concurrently(() =>
                    {
                        if (ReferenceEquals(clientCts, pending) || (ReferenceEquals(clientCts, current) && pending is null))
                            wrapperTask.TrySetResult(new Error("The client stopped listening without an error."));
                        return Task.CompletedTask;
                    })(clientCts.Token);
                }
                catch (Exception ex)
                {
                    wrapperTask.TrySetException(ex);
                }
                finally
                {
                    clientCts.Dispose();
                }
            }

            Task<Validation> startAndSetPending(EventSubWebsocketUrl url)
            {
                CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(wrapperCancellationToken);
                return setPending(cts).BindAsync(async _ => { await startNewClient(cts, url); return new Validation(); }, CancellationToken.None);
            }

            ProcessWebsocketMessage createReconnectPipelineFor(CancellationTokenSource clientCts)
            {
                Task<Validation> tryPromoteToCurrent() => semaphore.Concurrently(async () =>
                    {
                        if (!ReferenceEquals(pending, clientCts))
                        {
                            // This client might be current, so:
                            if (!ReferenceEquals(current, clientCts))
                            {
                                // Cleanup orphaned instance.
                                await clientCts.CancelAsync();
                            }
                            return;
                        }
                        if (current is not null)
                        {
                            // Safe to stop the current instance.
                            await current.CancelAsync();
                        }
                        // Promote
                        current = pending;
                        pending = null;
                    })(clientCts.Token);

                return pipeline.With(next => async (messageStream, messageCt) =>
                {
                    Validation<EventSubWebsocketMessage> message = await next(messageStream, messageCt);
                    // This is essentially a side effect of the pipeline, it does not change the output of next.
                    // We could have it return the message back to make a one-liner, but that's probably too confusing.
                    _ = message.Match(
                        onError: e => Task.CompletedTask, // no effect on error.
                        onValid: message =>
                        {
                            switch (message)
                            {
                                // Errors on these functions are from task cancellation, which we don't care about here.
                                // Cancellation will be picked up by the clients internally and hit the Match we set up when started.
                                case EventSubWebsocketMessage<ReconnectMessagePayload> reconnectMessage:
                                    _ = startAndSetPending(reconnectMessage.Payload.Session.ReconnectUrl);
                                    break;
                                case EventSubWebsocketMessage<WelcomeMessagePayload> welcomeMessage when pending is not null:
                                    _ = tryPromoteToCurrent();
                                    break;
                                default:
                                    break;
                            }
                            return Task.CompletedTask;
                        }
                        );

                    return message;
                });
            }

            _ = startAndSetPending(url);
            await wrapperTask.Task; // This should never complete unless cancelled or an error occurs.
        };
    }
}
