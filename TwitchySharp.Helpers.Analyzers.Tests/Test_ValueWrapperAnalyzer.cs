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
            public partial record Warns_ForWrongTypeRecordConstructor(int {|VWG0004:Value|})
            {
                public static Warns_ForWrongTypeRecordConstructor Create(string value)
                    => new(int.Parse(value));
            }
            """
        },
        new AnalyzerTestCase()
        {
            Name = "Warns_ForWrongTypeValueProperty",
            Input = """
            namespace TestNamespace;
            using TwitchySharp.Helpers;

            [Wrapper<string>]
            public partial record Warns_ForWrongTypeValueProperty
            {
                public int {|VWG0004:Value|} { get; init; }
            }
            """
        },
        new AnalyzerTestCase()
        {
            Name = "Warns_ForNonInitializableValueProperty",
            Input = """
            namespace TestNamespace;
            using TwitchySharp.Helpers;

            [Wrapper<string>]
            public partial record {|VWG0003:Warns_ForNonInitializableValueProperty|}
            {
                public string Value { get; }
            }
            """
        },
        new AnalyzerTestCase()
        {
            Name = "Warns_ForConstructorWithMultipleParameters",
            Input = """
            namespace TestNamespace;
            using TwitchySharp.Helpers;

            [Wrapper<string>]
            public partial record {|VWG0003:Warns_ForConstructorWithMultipleParameters|}(string Value, int Amount);
            """
        },
        new AnalyzerTestCase()
        {
            Name = "DoesNotWarn_ForNonInitializableValuePropertyWithValueConstructor",
            Input = """
            namespace TestNamespace;
            using TwitchySharp.Helpers;

            [Wrapper<string>]
            public partial record DoesNotWarn_ForNonInitializableValuePropertyWithValueConstructor
            {
                public DoesNotWarn_ForNonInitializableValuePropertyWithValueConstructor(string val)
                    => Value = val;
                public string Value { get; }
            }
            """
        },
        new AnalyzerTestCase()
        {
            Name = "DoesNotWarn_ForOtherRequiredPropertiesWithSuitableCreateMethod",
            Input = """
            namespace TestNamespace;
            using TwitchySharp.Helpers;

            [Wrapper<string>]
            public partial record DoesNotWarn_ForOtherRequiredPropertiesWithSuitableCreateMethod
            {
                public required string Value { get; init; }
                public required int Amount { get; init; }
                public static DoesNotWarn_ForOtherRequiredPropertiesWithSuitableCreateMethod Create(string value)
                    => new() { Amount = int.Parse(value), Value = value };
            }
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
