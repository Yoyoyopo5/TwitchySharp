using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Tests.E2E;
using Xunit.Sdk;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.EventSub;

public static class EventSubSubscriptionTypeTestExtensions
{
    public static TestName ToTestName(this EventSubSubscriptionType subscriptionType)
    {
        const string EVENT_SUB_TEST_PREFIX = "eventsub-";
        return new($"{EVENT_SUB_TEST_PREFIX}{subscriptionType.Type}.{subscriptionType.Version}");
    }
}

public class EventSubTestRow() : IXunitSerializable
{
    public TestName TestName => SubscriptionType.ToTestName();
    public required EventSubSubscriptionType SubscriptionType { get; set; }

    public void Deserialize(IXunitSerializationInfo info)
    {
        SubscriptionType = new(
            Type: new(info.GetValue<string>(nameof(SubscriptionType.Type)) ?? "unknown"),
            Version: new(info.GetValue<string>(nameof(SubscriptionType.Version)) ?? "unknown")
            );
    }
    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(SubscriptionType.Type), SubscriptionType.Type.Value);
        info.AddValue(nameof(SubscriptionType.Version), SubscriptionType.Version.Value);
    }

    public override string ToString() => TestName;
}

public abstract record EventSubTest
{
    public abstract EventSubSubscriptionTypeSpecification WithFixture(TwitchClientFixture fixture);
}

public sealed record EventSubTest<TRequiredIdentity, TSpecification> : EventSubTest
    where TSpecification : EventSubSubscriptionTypeSpecification, IConditionConstructable<TSpecification>
    where TRequiredIdentity : ITestIdentity
{
    public required Func<TRequiredIdentity, TSpecification> CreateSpecification { get; init; }
    public override EventSubSubscriptionTypeSpecification WithFixture(TwitchClientFixture fixture)
        => CreateSpecification(fixture.GetAuthorizingConfigForTestOrSkip<TRequiredIdentity>(TSpecification.SubscriptionType.ToTestName()));
}
