using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Tests.Unit;

public partial class Test_Wrapper
{
    [Wrapper<string>]
    private partial record TestStringWrapper(string Value);

    [Fact]
    public void StringImplicitCast_StringValue_ReturnStringValue()
    {
        const string MOCK_STRING = "mock";

        TestStringWrapper stubEnum = new(MOCK_STRING);

        string actual = stubEnum;

        Assert.Equal(MOCK_STRING, actual);
    }
}
