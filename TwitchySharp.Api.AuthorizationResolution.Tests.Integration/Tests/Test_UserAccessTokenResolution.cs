using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution.Tests.Integration.Tests;

public class Test_UserAccessTokenResolution(TokenResolutionTestFixture fixture)
    : IClassFixture<TokenResolutionTestFixture>
{
    private readonly TokenResolutionTestFixture _fixture = fixture;

    [Fact]
    public async Task SendRequest_WithExpiredCachedTokenWithRefreshToken_RefreshedTokenCreatedAndUsed()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public async Task SendRequest_WithExpiredCachedTokenWithoutRefreshToken_ExpiredTokenUsed()
    {
        throw new NotImplementedException();
    }
}
