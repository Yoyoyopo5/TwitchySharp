using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Tests.E2E;
using Xunit.Sdk;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.EventSub;

public abstract class EventSubTest : IXunitSerializable
{
    public required TestName TestName { get; set; }

    public abstract IEventSubSubscriptionTypeSpecification WithFixture(TwitchClientFixture fixture);

    public void Deserialize(IXunitSerializationInfo info)
    {
        // This is going to cause issues at some point.
        TestName = new(info.GetValue<string>(nameof(TestName)) ?? "unknown");
    }
    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(TestName), TestName.Value);
    }
}

public sealed class EventSubTest<TSubscription, TRequiredIdentity> : EventSubTest
    where TSubscription : IEventSubSubscriptionTypeSpecification
    where TRequiredIdentity : ITestIdentity
{
    public required Func<TRequiredIdentity, TSubscription> CreateSpecification { get; init; }
    public override IEventSubSubscriptionTypeSpecification WithFixture(TwitchClientFixture fixture)
        => CreateSpecification(fixture.GetAuthorizingConfigForTestOrSkip<TRequiredIdentity>(TestName));
}
