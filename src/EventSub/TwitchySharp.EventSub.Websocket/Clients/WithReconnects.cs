using TwitchySharp.EventSub.Websocket.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Websocket.Clients;

public static class StartEventSubWebsocketClientExtensions
{
    /// <summary>
    /// Wraps an EventSub Websocket client to support seamless automatic reconnect message handling.
    /// </summary>
    /// <remarks>
    /// This may call <paramref name="startClient"/> and its resulting stop function multiple times
    /// over its lifecycle and expects an implementation that supports being called multiple times,
    /// each starting its own connection. Up to two clients may be active at one time to allow for
    /// seamless reconnect transitions.
    /// </remarks>
    /// <param name="startClient">
    /// The base client to use.
    /// </param>
    /// <returns>A <see cref="StartEventSubWebsocketClient"/> that automatically handles reconnect messages.</returns>
    public static StartEventSubWebsocketClient WithReconnects(
        this StartEventSubWebsocketClient startClient,
        Action<Exception>? onReconnectError = null
        )
    {
        return async (pipeline, url, ct) =>
        {
            SemaphoreSlim semaphore = new(1, 1);

            StopWebsocketClient? current = null;
            StopWebsocketClient? pending = null;

            // disposed during stop
            CancellationTokenSource wrapperCts = new();
            // register on the start cancellation token so if we cancel during startup (before we get the stop function),
            // the wrapper cts is still disposed (but it is not disposed if we successfully start).
            await using CancellationTokenRegistration disposeWrapperCts = ct.Register(() => wrapperCts.Dispose());

            async Task stop(CancellationToken ct)
            {
                // Cancels any ongoing handoff operation.
                await wrapperCts.CancelAsync();

                await semaphore.Concurrently(async () =>
                {
                    await (current?.Invoke(ct) ?? Task.CompletedTask);
                    await (pending?.Invoke(ct) ?? Task.CompletedTask);
                })(ct);

                wrapperCts.Dispose();
            }

            Task setPending(StopWebsocketClient reconnectClient, CancellationToken ct)
                => semaphore.Concurrently(async () =>
                {
                    if (pending is not null)
                        await pending(ct);
                    pending = reconnectClient;
                })(ct);

            async Task<StopWebsocketClient> startNewClient(EventSubWebsocketUrl url, CancellationToken ct)
            {
                // We initialize a StopWebsocketClient variable to pass into the pipeline.
                // This is then set when the client is actually started, so the pipeline
                // should never see it as a null reference.
                StopWebsocketClient? stopClient = null;
                stopClient = await startClient(createReconnectPipelineFor(() => stopClient!), url, ct);
                return stopClient;
            }

            async Task startAndSetPending(EventSubWebsocketUrl url, CancellationToken ct)
                => await setPending(await startNewClient(url, ct), ct);

            ProcessWebsocketMessage createReconnectPipelineFor(Func<StopWebsocketClient> getClient)
            {
                Task promoteToCurrent(CancellationToken ct) => semaphore.Concurrently(async () =>
                    {
                        StopWebsocketClient client = getClient();
                        if (!ReferenceEquals(pending, client))
                        {
                            // This client might be current, so:
                            if (!ReferenceEquals(current, client))
                            {
                                // Cleanup orphaned instance.
                                await client(ct);
                            }
                            return;
                        }
                        if (current is not null)
                        {
                            // Safe to stop the current instance.
                            await current(ct);
                        }
                        // Promote
                        current = pending;
                        pending = null;
                    })(ct);

                return pipeline
                    .MapReconnect((reconnectSession, ct) =>
                    {
                        _ = Task.Run(WithTry(
                                () => startAndSetPending(reconnectSession.ReconnectUrl, wrapperCts.Token),
                                onReconnectError.IgnoreCancellationExceptions()
                                ), wrapperCts.Token);
                        return ValueTask.CompletedTask;
                    })
                    .MapWelcome((session, ct) =>
                    {
                        if (pending is not null)
                            _ = Task.Run(WithTry(
                                    () => promoteToCurrent(wrapperCts.Token),
                                    onReconnectError.IgnoreCancellationExceptions()
                                    ), wrapperCts.Token);
                        return ValueTask.CompletedTask;
                    });
            }

            await startAndSetPending(url, ct);
            return stop;
        };
    }

    // Helper extensions
    private static Func<Task> WithTry(this Func<Task> func, Action<Exception>? @catch)
    => async () =>
    {
        try
        {
            await func();
        }
        catch (Exception ex)
        {
            if (@catch is null)
                throw;
            @catch(ex);
        }
    };

    private static Action<Exception>? IgnoreCancellationExceptions(this Action<Exception>? @catch)
        => @catch is not null ? ex =>
        {
            switch (ex)
            {
                case OperationCanceledException or TaskCanceledException:
                    break;
                default:
                    @catch(ex);
                    break;
            }
        }
    : null;

    private static Func<CancellationToken, Task> Concurrently(this SemaphoreSlim semaphore, Func<Task> func)
        => async ct =>
        {
            await semaphore.WaitAsync(ct);
            try
            {
                await func();
            }
            finally
            {
                semaphore.Release();
            }
        };
}
