using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Tests.Unit;
public class Test_HttpQueryParameters
{
    [Theory]
    [InlineData("key", "value")]
    public void Add_SingleStringParameter_ReturnParametersString(string key, string value)
    {
        string mockParametersString = $"?{key}={value}";

        string actual = new HttpQueryParameters()
            .Add(key, value)
            .ToString();

        Assert.Equal(mockParametersString, actual);
    }

    [Theory]
    [InlineData("key", "value", "another_key", "another_value")]
    [InlineData("key", "value", "key", "value")]
    public void Add_MultipleStringParameters_ReturnParametersString(string key1, string value1, string key2, string value2)
    {
        string mockParametersString = $"?{key1}={value1}&{key2}={value2}";

        string actual = new HttpQueryParameters()
            .Add(key1, value1)
            .Add(key2, value2)
            .ToString();

        Assert.Equal(mockParametersString, actual);
    }

    public void Add_SingleEnumerableParameter_ReturnParametersString(string key, string value1, string value2)
    {
        string mockParametersString = $"?{key}={value1}&{key}={value2}";

        string actual = new HttpQueryParameters()
            .Add(key, [value1, value2])
            .ToString();

        Assert.Equal(mockParametersString, actual);
    }

    [Fact]
    public void Add_NullParameterValue_ReturnParametersString()
    {
        const string STUB_NON_NULL_PARAMETER_KEY = "non_null_key";
        const string STUB_NON_NULL_PARAMETER_VALUE = "non_null_value";
        const string STUB_NULL_PARAMETER_KEY = "null_key";

        string mockParametersString = $"?non_null_key=non_null_value";

        string actual = new HttpQueryParameters()
            .Add(STUB_NON_NULL_PARAMETER_KEY, STUB_NON_NULL_PARAMETER_VALUE)
            .Add(STUB_NULL_PARAMETER_KEY, (string?)null)
            .ToString();

        Assert.Equal(mockParametersString, actual);
    }
}
