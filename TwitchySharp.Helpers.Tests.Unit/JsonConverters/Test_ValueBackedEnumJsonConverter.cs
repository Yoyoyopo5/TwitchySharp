using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Tests.Unit.JsonConverters;
public class Test_ValueBackedEnumJsonConverter
{
    private record TestStringEnum(string Value) : ValueBackedEnum<string>(Value);
    private ValueBackedEnumJsonConverter<TestStringEnum, string> _converter = new();

    private const string FAKE_STRING_VALUE = "fake_value";
    private const string FAKE_JSON = $"""
        "{FAKE_STRING_VALUE}"
        """;

    [Fact]
    public void Read_StringValue_ReturnsStringEnum()
    {
        const string STUB_JSON = FAKE_JSON;
        const string STUB_STRING_VALUE = FAKE_STRING_VALUE;
        TestStringEnum mockEnum = new(STUB_STRING_VALUE);

        TestStringEnum? actual = _converter.Read(STUB_JSON);

        Assert.Equal(mockEnum, actual);
    }

    [Fact]
    public void Write_StringEnum_ReturnsJsonString()
    {
        const string STUB_STRING_VALUE = FAKE_STRING_VALUE;
        TestStringEnum stubEnum = new(STUB_STRING_VALUE);
        const string MOCK_JSON = FAKE_JSON;

        string actual = _converter.Write(stubEnum);

        Assert.Equal(MOCK_JSON, actual);
    }
}