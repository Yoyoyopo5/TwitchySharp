using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using Xunit.Sdk;

namespace TwitchySharp.Helpers.Analyzers.Tests;

public record AnalyzerTestCase : ITestData<AnalyzerTestCase>
{
    public required string Name { get; init; }
    public required string Input { get; init; }

    public CSharpAnalyzerTest<TAnalyzer, TVerifier> Register<TAnalyzer, TVerifier>(CSharpAnalyzerTest<TAnalyzer, TVerifier> test)
        where TAnalyzer : DiagnosticAnalyzer, new()
        where TVerifier : IVerifier, new()
    {
        test.TestCode = Input;
        return test;
    }

    public IXunitSerializationInfo Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(Name), Name);
        info.AddValue(nameof(Input), Input);
        return info;
    }

    public static AnalyzerTestCase Deserialize(IXunitSerializationInfo info)
    => new()
    {
        Name = info.GetValue<string>(nameof(Name)) ?? throw new ArgumentNullException(nameof(info), "Name cannot be null"),
        Input = info.GetValue<string>(nameof(Input)) ?? throw new ArgumentNullException(nameof(info), "Input cannot be null")
    };

    public override string ToString() => Name;
};
