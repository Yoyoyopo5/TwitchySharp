using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Xunit.Sdk;

namespace TwitchySharp.Helpers.Generators.Tests;

public record SourceGeneratorTestCase : ITestData<SourceGeneratorTestCase>
{
    public required string Name { get; init; }
    public required string Input { get; init; }
    public required string? ExpectedOutput { get; init; }
    public required string? ExpectedOutputFilePath { get; init; }

    public CSharpSourceGeneratorTest<TSourceGenerator, TVerifier> Register<TSourceGenerator, TVerifier>(CSharpSourceGeneratorTest<TSourceGenerator, TVerifier> test)
        where TSourceGenerator : new()
        where TVerifier : IVerifier, new()
    {
        test.TestCode = Input;
        if (ExpectedOutputFilePath is null || ExpectedOutput is null)
            return test;
        test.TestState.GeneratedSources.Add((typeof(ValueWrapperGenerator), ExpectedOutputFilePath, ExpectedOutput));
        return test;
    }

    public IXunitSerializationInfo Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(Name), Name);
        info.AddValue(nameof(Input), Input);
        info.AddValue(nameof(ExpectedOutput), ExpectedOutput);
        info.AddValue(nameof(ExpectedOutputFilePath), ExpectedOutputFilePath);
        return info;
    }

    public static SourceGeneratorTestCase Deserialize(IXunitSerializationInfo info)
    => new()
    {
        Name = info.GetValue<string>(nameof(Name)) ?? throw new ArgumentNullException(nameof(info), "Name cannot be null"),
        Input = info.GetValue<string>(nameof(Input)) ?? throw new ArgumentNullException(nameof(info), "Input cannot be null"),
        ExpectedOutput = info.GetValue<string>(nameof(ExpectedOutput)),
        ExpectedOutputFilePath = info.GetValue<string>(nameof(ExpectedOutputFilePath))
    };

    public override string ToString() => Name;
};
