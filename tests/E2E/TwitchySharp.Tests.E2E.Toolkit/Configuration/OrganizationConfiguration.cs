namespace TwitchySharp.Tests.E2E;

public class OrganizationConfiguration : ITestIdentity
{
    public required OrganizationId OrganizationId { get; set; }
    public required HashSet<TestName> Tests { get; set; }
    IReadOnlySet<TestName> ITestIdentity.Tests => Tests;
}
