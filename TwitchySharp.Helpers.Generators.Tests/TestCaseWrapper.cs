using Xunit.Sdk;

namespace TwitchySharp.Helpers.Generators.Tests;

public record TestCaseWrapper<T> : IXunitSerializable
    where T : ITestData<T>
{
    public T TestCase { get; private set; }
    public TestCaseWrapper() => TestCase = default!;
    public TestCaseWrapper(T testCase) => TestCase = testCase;
    public void Serialize(IXunitSerializationInfo info)
        => TestCase.Serialize(info);
    public void Deserialize(IXunitSerializationInfo info)
        => T.Deserialize(info);
    public override string ToString() => TestCase?.ToString() ?? "Unknown";
}

public interface ITestData<T>
{
    IXunitSerializationInfo Serialize(IXunitSerializationInfo info);
    static abstract T Deserialize(IXunitSerializationInfo info);
}
