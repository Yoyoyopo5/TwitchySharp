using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using TwitchySharp.Helpers.Analyzers.IWrapValue;

namespace TwitchySharp.Helpers.Analyzers.Tests;

public class Test_ValueWrapperAnalyzer
{
    private static CSharpAnalyzerTest<ValueWrapperAnalyzer, DefaultVerifier> CreateGeneratorTest()
    {
        CSharpAnalyzerTest<ValueWrapperAnalyzer, DefaultVerifier> test = new()
        {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80
        };
        test.TestState.AdditionalReferences.Add(typeof(WrapperAttribute<>).Assembly);
        return test;
    }

    private readonly static AnalyzerTestCase[] _testCase =
    [
        new AnalyzerTestCase()
        {
            Name = "Warns_ForNonPartialClass",
            Input = """
            namespace TestNamespace;
            using TwitchySharp.Helpers;

            [Wrapper<string>]
            public class {|VWG0001:Warns_ForNonPartialClass|} { }
            """
        },
        new AnalyzerTestCase()
        {
            Name = "Warns_ForNonPartialParentClass",
            Input = """
            namespace TestNamespace;
            using TwitchySharp.Helpers;

            public class {|VWG0002:Container|}
            {
                [Wrapper<int>]
                private readonly partial record struct Warns_ForNonPartialParentClass(int Value);
            }
            """
        },
        new AnalyzerTestCase()
        {
            Name = "Warns_ForWrongTypeRecordConstructor",
            Input = """
            namespace TestNamespace;
            using TwitchySharp.Helpers;

            [Wrapper<string>]
            public partial record Warns_ForWrongTypeRecordConstructor(int Value);
            """
        }
    ];

    public static TheoryData<TestCaseWrapper<AnalyzerTestCase>> TestCases
        => [.. _testCase.Select(t => new TestCaseWrapper<AnalyzerTestCase>(t))];

    [MemberData(nameof(TestCases))]
    [Theory]
    public async Task Analyzer(TestCaseWrapper<AnalyzerTestCase> @case)
        => await @case.TestCase.Register(CreateGeneratorTest()).RunAsync(TestContext.Current.CancellationToken);
}
