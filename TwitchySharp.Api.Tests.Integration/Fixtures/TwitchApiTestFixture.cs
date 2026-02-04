using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using TwitchySharp.Api.Tests.Integration.Controllers;
using TwitchySharp.Api.Tests.Integration.Models;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Integration.Fixtures;

/// <summary>
/// Stub program class for WebApplicationFactory.
/// </summary>
public class Program { }

/// <summary>
/// Test fixture providing a mock Twitch API server for integration tests.
/// </summary>
public class TwitchApiTestFixture : WebApplicationFactory<Program>
{
    /// <summary>
    /// Allows tests to configure mock responses.
    /// </summary>
    public MockResponseConfigurator ResponseConfig { get; } = new();

    // Test constants
    public const string TestClientId = "test_client_id";
    public const string TestClientSecret = "test_client_secret";
    public const string TestAccessToken = "test_access_token";
    public const string TestRefreshToken = "test_refresh_token";
    public const string TestAuthorizationCode = "test_auth_code";
    public const string TestRedirectUri = "http://localhost:3000";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton(ResponseConfig);
            services.AddControllers()
                .AddApplicationPart(typeof(MockAuthorizationController).Assembly);
        });

        builder.Configure(app =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        });
    }

    protected override IHostBuilder? CreateHostBuilder() =>
        Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseTestServer());

    public DefaultRequestAuthorizer CreateDefaultAuthorizer() =>
        new(
            new DefaultClientIdentityResolver(new ClientIdentity(new ClientId(TestClientId))),
            new SingleAccessTokenResolver(new UserAccessToken(TestAccessToken))
        );

    /// <summary>
    /// Creates a TwitchClient configured to use the test server.
    /// </summary>
    /// <param name="authorizer">Optional request authorizer. Pass null for authorization endpoints that don't need auth.</param>
    /// <returns>A TwitchClient configured to use the mock server.</returns>
    public TwitchClient CreateTwitchClient(IAuthorizeTwitchRequest? authorizer = null) =>
        new(CreateClient(), authorizer);

    /// <summary>
    /// Creates an HttpClient configured to use the test server directly.
    /// </summary>
    /// <returns>An HttpClient that sends requests to the mock server.</returns>
    public new HttpClient CreateClient() =>
        CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
}
