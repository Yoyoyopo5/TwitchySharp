using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Tests.Unit.Authorization;
public class Test_Scope
{
    [Fact]
    public void FormatScopes_SingleScope_ReturnsScopeString()
    {
        const string MOCK_SCOPE_STRING = "analytics:read:extensions";

        IEnumerable<Scope> stubScopes = [Scope.AnalyticsReadExtensions];

        string result = stubScopes.FormatScopes();

        Assert.Equal(MOCK_SCOPE_STRING, result);
    }
}
