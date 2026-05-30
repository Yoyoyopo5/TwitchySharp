using TwitchySharp.EventSub.Websocket.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Websocket.Clients;

internal delegate Task<Validation> ListenToEventSubWebsocketClient(ProcessWebsocketMessage pipeline, EventSubWebsocketUrl url, CancellationToken ct); // Cancellation token handles entire lifetime

internal static class EventSubWebsocketClient
{
    public static ListenToEventSubWebsocketClient WithReconnects(this ListenToEventSubWebsocketClient listenToClient)
    {
        return async (pipeline, url, wrapperCancellationToken) =>
        {
            // TODO: Introduce semaphore on current/pending state transitions
            TaskCompletionSource<Validation> wrapperTask = new();
            await using CancellationTokenRegistration registration = wrapperCancellationToken.Register(() => wrapperTask.TrySetCanceled(wrapperCancellationToken));

            CancellationTokenSource? current = null;
            CancellationTokenSource? pending = null;

            ProcessWebsocketMessage CreateReconnectPipelineFor(CancellationTokenSource clientCts)
                => pipeline.With(next => async (messageStream, messageCt) =>
                {
                    Validation<EventSubWebsocketMessage> message = await next(messageStream, messageCt);
                    // This is essentially a side effect of the pipeline, it does not change the output of next.
                    // We could have it return the message back to make a one-liner, but that's probably too confusing.
                    await message.Match<ValueTask>(
                        onError: e => ValueTask.CompletedTask, // no effect on error.
                        onValid: async message =>
                        {
                            switch (message)
                            {
                                case EventSubWebsocketMessage<ReconnectMessagePayload> reconnectMessage:
                                    if (pending is not null)
                                    {
                                        await pending.CancelAsync();
                                        pending.Dispose();
                                    }
                                    CancellationTokenSource reconnectCts = CancellationTokenSource.CreateLinkedTokenSource(wrapperCancellationToken);
                                    pending = reconnectCts;
                                    _ = listenToClient(CreateReconnectPipelineFor(reconnectCts), reconnectMessage.Payload.Session.ReconnectUrl, reconnectCts.Token)
                                        .MatchAsync(
                                            onError: (e, _) =>
                                            {
                                                reconnectCts.Dispose();
                                                wrapperTask.TrySetResult(e);
                                                return Task.CompletedTask;
                                            },
                                            onValid: _ =>
                                            {
                                                try
                                                {
                                                    if (reconnectCts.IsCancellationRequested)
                                                        return Task.CompletedTask;

                                                    if (ReferenceEquals(reconnectCts, pending) || (ReferenceEquals(reconnectCts, current) && pending is null))
                                                        wrapperTask.TrySetResult(new Error("The client stopped listening without an error."));
                                                    return Task.CompletedTask;
                                                }
                                                finally
                                                {
                                                    reconnectCts.Dispose();
                                                }
                                            },
                                            reconnectCts.Token
                                            );
                                    break;
                                case EventSubWebsocketMessage<WelcomeMessagePayload> welcomeMessage when pending is not null:
                                    if (!ReferenceEquals(pending, clientCts))
                                        break;
                                    if (current is not null)
                                    {
                                        await current.CancelAsync();
                                        current.Dispose();
                                    }
                                    current = pending;
                                    pending = null;
                                    break;
                                default:
                                    break;
                            }
                        }
                        );

                    return message;
                });

            CancellationTokenSource initial = CancellationTokenSource.CreateLinkedTokenSource(wrapperCancellationToken);
            pending = initial;
            _ = listenToClient(CreateReconnectPipelineFor(initial), url, initial.Token).MatchAsync(
                onError: (e, _) =>
                {
                    initial.Dispose();
                    wrapperTask.TrySetResult(e);
                    return Task.CompletedTask;
                },
                onValid: _ =>
                {
                    try
                    {
                        if (initial.IsCancellationRequested)
                            return Task.CompletedTask;

                        if (ReferenceEquals(initial, pending) || (ReferenceEquals(initial, current) && pending is null))
                            wrapperTask.TrySetResult(new Error("The client stopped listening without an error."));
                        return Task.CompletedTask;
                    }
                    finally
                    {
                        initial.Dispose();
                    }
                },
                initial.Token
                );

            return await wrapperTask.Task; // This should never complete unless cancelled or an error occurs.
        };
    }
}
