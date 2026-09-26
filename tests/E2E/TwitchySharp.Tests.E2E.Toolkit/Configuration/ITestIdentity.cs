using TwitchySharp.Api;

namespace TwitchySharp.Tests.E2E;

public interface ITestIdentity<out TIdentity>
    where TIdentity : TwitchIdentity
{
    TIdentity Identity { get; }
    IReadOnlySet<TestName> Tests { get; }
}

public static class IEnableEndpointsExtensions
{
    public static T? WithTestName<T>(this IEnumerable<T> users, TestName endpoint)
        where T : ITestIdentity<TwitchIdentity>
        => users.FirstOrDefault(u => u.Tests.Contains(endpoint));
}
