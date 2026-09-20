using System.Collections.Concurrent;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Tests.Unit.Toolkit;
using static TwitchySharp.Tests.Unit.Toolkit.Concurrency;

namespace TwitchySharp.Api.Tests.Unit.Client.RateLimiting;

public class Test_SerializeRequestsByClientId
{
    private static TwitchClient CreateStubClient()
        => new TwitchClient() { Resolvers = new ImmutableRequestDependencyCollection() }
            .SetResolver<TwitchClient, HttpResponseMessage?>(async (scope, ct) =>
            {
                if (scope.Request is ConcurrentRequest cr)
                    await cr.Effect();
                return null;
            })
            .SetResolver<TwitchClient, TwitchResponse<object>>(async (scope, ct) =>
            {
                await scope.ResolveOrDefault<HttpResponseMessage>(ct); // Invokes serializer
                return new TwitchResponse<object>()
                {
                    Content = new(),
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Request = null!
                };
            });

    private record ConcurrentRequest(Func<Task> Effect) : StubTwitchRequest;

    [Fact]
    public async Task SendAsync_WithNoClientId_RequestsRunInParallel()
    {
        const int WORKER_COUNT = 64;
        Assert.True(WORKER_COUNT > 1);
        TimeSpan workerDelay = TimeSpan.FromMilliseconds(10);
        CancellationToken ct = TestContext.Current.CancellationToken;
        Concurrency.Probe probe = new();

        TwitchClient stubClient = CreateStubClient()
            .SerializeRequestsByClientId();

        await Concurrency.RunConcurrently(WORKER_COUNT, async i =>
        {
            await stubClient.SendAsync(new ConcurrentRequest(async () =>
            {
                using IDisposable serializationBoundary = probe.Enter();
                await Task.Delay(workerDelay);
            }), ct);
        }, ct);

        probe.AssertParallelExecution();
    }

    [Fact]
    public async Task SendAsync_WithSameClientId_RequestsRunInSerial()
    {
        const int WORKER_COUNT = 64;
        Assert.True(WORKER_COUNT > 1);
        TimeSpan workerDelay = TimeSpan.FromMicroseconds(100);
        CancellationToken ct = TestContext.Current.CancellationToken;
        Concurrency.Probe probe = new();

        TwitchClient stubClient = CreateStubClient()
            .SetFixed<TwitchClient, ClientId?>(new ClientId("12345"))
            .SerializeRequestsByClientId();

        await Concurrency.RunConcurrently(WORKER_COUNT, async i =>
        {
            await stubClient.SendAsync(new ConcurrentRequest(async () =>
            {
                using IDisposable serializationBoundary = probe.Enter();
                await Task.Delay(workerDelay);
            }), ct);
        }, ct);

        probe.AssertSerialExecution();
    }

    [Fact]
    public async Task SendAsync_WithDifferentClientIds_RequestsRunInParallel()
    {
        const int WORKER_COUNT = 64;
        Assert.True(WORKER_COUNT > 1);
        TimeSpan workerDelay = TimeSpan.FromMilliseconds(10);
        CancellationToken ct = TestContext.Current.CancellationToken;
        Concurrency.Probe probe = new();

        TwitchClient stubClient = CreateStubClient()
            .SerializeRequestsByClientId();

        await Concurrency.RunConcurrently(WORKER_COUNT, async i =>
        {
            await stubClient
                .SetFixed<TwitchClient, ClientId?>(new ClientId(i.ToString()))
                .SendAsync(new ConcurrentRequest(async () =>
                {
                    using IDisposable serializationBoundary = probe.Enter();
                    await Task.Delay(workerDelay);
                }), ct);
        }, ct);

        probe.AssertParallelExecution();
    }

    private class StubAsyncDisposable : IAsyncDisposable
    {
        public bool Disposed { get; private set; }
        public async ValueTask DisposeAsync() => Disposed = true;
    }

    [Fact]
    public async Task SendAsync_WithLockFactory_LockDisposed()
    {
        ClientId fakeClientId = new("12345");
        StubAsyncDisposable @lock = new();

        TwitchClient stubClient = CreateStubClient()
            .SetFixed<TwitchClient, ClientId?>(fakeClientId)
            .SerializeRequestsByClientId((clientId, _) =>
            {
                Assert.Equal(fakeClientId, clientId);
                return ValueTask.FromResult<IAsyncDisposable>(@lock);
            });

        await stubClient.SendAsync(new StubTwitchRequest(), TestContext.Current.CancellationToken);

        Assert.True(@lock.Disposed);
    }
}
