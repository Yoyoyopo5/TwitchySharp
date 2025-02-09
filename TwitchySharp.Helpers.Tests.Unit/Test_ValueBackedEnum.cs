using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Tests.Unit;
public class Test_ValueBackedEnum
{
    private record TestStringEnum(string Value) : ValueBackedEnum<string>(Value);

    [Fact]
    public void StringImplicitCast_StringValue_ReturnStringValue()
    {
        const string MOCK_STRING = "mock";

        TestStringEnum stubEnum = new(MOCK_STRING);

        string actual = stubEnum;

        Assert.Equal(MOCK_STRING, actual);
    }
}
