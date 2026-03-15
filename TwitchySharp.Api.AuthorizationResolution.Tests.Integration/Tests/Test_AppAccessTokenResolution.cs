using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution.Tests.Integration.Tests;

public class Test_AppAccessTokenResolution(TokenResolutionTestFixture fixture)
    : IClassFixture<TokenResolutionTestFixture>
{
    private readonly TokenResolutionTestFixture _fixture = fixture;

    [Fact]
    public async Task SendRequest_WithUnavailableCachedToken_NewTokenCreatedAndUsed()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public async Task SendRequest_WithExpiredCachedToken_NewTokenCreatedAndUsed()
    {
        throw new NotImplementedException();
    }
}
