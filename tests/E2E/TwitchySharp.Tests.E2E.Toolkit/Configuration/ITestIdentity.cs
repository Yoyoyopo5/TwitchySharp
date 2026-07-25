namespace TwitchySharp.Tests.E2E;

public interface ITestIdentity
{
    IReadOnlySet<TestName> Tests { get; }
}

public static class IEnableEndpointsExtensions
{
    public static T? WithTestName<T>(this IEnumerable<T> users, TestName endpoint)
        where T : ITestIdentity
        => users.FirstOrDefault(u => u.Tests.Contains(endpoint));
}
