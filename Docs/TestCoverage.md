# Test Coverage Overview

> Quick reference for what is and isn't covered by unit tests

---

## Summary

| Project | Source Files | Unit Tests | Coverage |
|---------|--------------|------------|----------|
| TwitchySharp.Api | 472 | 4 | ~1% |
| TwitchySharp.Helpers | 11 | 4 | ~36% |
| TwitchySharp.EventSub | 7 | 1 | ~14% |
| TwitchySharp.EventSub.Websocket | 13 | 0 | 0% |
| TwitchySharp.EventSub.Webhooks | 6 | 0 | 0% |

**Note:** Integration tests exist separately and cover API endpoint calls against the real Twitch API.

---

## What's Tested

### TwitchySharp.Api.Tests.Unit

| File | What It Tests | Status |
|------|---------------|--------|
| `Test_Scope.cs` | `Scope.FormatScopes()` - single & multiple scopes | Covered |
| `Test_OidcClaims.cs` | OIDC claim parsing | Covered |
| `Test_TwitchOidc.cs` | OIDC token validation | Covered |
| `Test_ExtensionJwtPayload.cs` | Extension JWT payload handling | Covered |

### TwitchySharp.Helpers.Tests.Unit

| File | What It Tests | Status |
|------|---------------|--------|
| `Test_HttpQueryParameters.cs` | Query string building | Covered |
| `Test_ValueBackedEnum.cs` | Enum-to-value mapping | Covered |
| `Test_ValueBackedEnumJsonConverter.cs` | JSON serialization of ValueBackedEnum | Covered |

### TwitchySharp.EventSub.Tests.Unit

| File | What It Tests | Status |
|------|---------------|--------|
| `Test_NotificationConverter.cs` | `AutomodMessageHold` deserialization | Covered |

---

## What's NOT Tested

### TwitchySharp.Api - Core Classes

| Class | Purpose | Priority |
|-------|---------|----------|
| `TwitchHttpClient` | Main HTTP client, rate limiting | High |
| `TwitchApiRequest<T>` | Base request class | High |
| `ApiException` | Error handling | Medium |
| `HttpResponseMessageTwitchExtensions` | Rate limit header parsing | Medium |
| `JsonConfig` | JSON serialization settings | Low |

### TwitchySharp.Api - Authorization (23 of 27 files untested)

| Category | Untested Classes |
|----------|------------------|
| Requests | `AccessTokenRefreshRequest`, `AuthorizationCodeRequest`, `ClientCredentialsRequest`, `DeviceCodeRequest`, `DeviceCodeTokenRequest`, `OidcJwkRequest`, `RevokeAccessTokenRequest`, `UserInfoRequest`, `ValidateAccessTokenRequest` |
| Responses | All response classes |
| Client URLs | `AuthorizationCodeGrantUrl`, `ImplicitGrantUrl`, `AuthorizationUrl` |

### TwitchySharp.Api - Helix (430 files, 0 unit tests)

All Helix endpoint request/response classes lack unit tests. Examples:

- `GetUsersRequest` / `GetUsersResponse`
- `GetChannelInformationRequest` / `GetChannelInformationResponse`
- `CreateClipRequest` / `CreateClipResponse`
- ... and 400+ more

**Note:** These are covered by integration tests, but unit tests would catch serialization issues without requiring API calls.

### TwitchySharp.Helpers (7 of 11 files untested)

| Class | Purpose | Priority |
|-------|---------|----------|
| `ImageUrlTemplate` | Image URL manipulation | Low |
| `TwitchDateTimeOffsetQueryConverterExtension` | DateTime query formatting | Medium |
| `EmptyDateTimeOffsetConverter` | JSON converter | Medium |
| `UnixSecondsDateTimeOffsetConverter` | JSON converter | Medium |
| `SnakeCaseLowerJsonStringEnumConverter` | JSON converter | Medium |
| `SnakeCaseUpperJsonStringEnumConverter` | JSON converter | Medium |
| `IntStringJsonConverter` | JSON converter | Medium |
| `MinutesTimeSpanJsonConverter` | JSON converter | Medium |
| `SecondsTimeSpanJsonConverter` | JSON converter | Medium |

### TwitchySharp.EventSub (6 of 7 files untested)

| Class | Purpose | Priority |
|-------|---------|----------|
| `NotificationConverter` | Only `AutomodMessageHold` tested | High |
| `ChannelChatMessage` | Chat message notification | High |
| `AutomodMessageHoldV2` | Updated automod notification | Medium |
| `EventSubSubscription` | Subscription metadata | Low |
| `EventSubTransport` | Transport metadata | Low |

### TwitchySharp.EventSub.Websocket (0% coverage)

| Class | Purpose | Priority |
|-------|---------|----------|
| `TwitchEventSubWebsocketClient` | WebSocket connection management | High |
| `EventSubWebsocketMessage` | Message deserialization | High |
| All payload classes | Message payloads | Medium |

### TwitchySharp.EventSub.Webhooks (0% coverage)

| Class | Purpose | Priority |
|-------|---------|----------|
| `DefaultEventSubWebhookMessageProcessor` | **Completely stubbed** (NotImplementedException) | N/A |
| `EventSubWebhookRequestHeader` | Header parsing | N/A |
| All request classes | Request handling | N/A |

**Note:** Webhooks implementation is incomplete - no point testing until implemented.

---

## Known Test Gaps

### Missing Test Scenarios

| Area | Missing Tests |
|------|---------------|
| `HttpQueryParameters` | URL encoding of special characters (`&`, `=`, spaces) |
| `TwitchHttpClient` | Rate limiting behavior, retry logic |
| Error handling | `ApiException` thrown on various HTTP status codes |
| JSON converters | Edge cases (null, empty, malformed JSON) |
| WebSocket | Connection lifecycle, reconnection, message ordering |

### Test Quality Issues

| File | Issue |
|------|-------|
| `Test_HttpQueryParameters.cs` | `Add_SingleEnumerableParameter_ReturnParametersString` missing `[Fact]`/`[Theory]` attribute |
| Integration tests | Many tests have no assertions (just call API) |

---

## Recommended Test Priorities

### Quick Wins (Low effort, High value)

1. **JSON Converters** - Add tests for all converters in `TwitchySharp.Helpers/JsonConverters/`
2. **More notification types** - Expand `Test_NotificationConverter.cs` to cover `ChannelChatMessage`
3. **Fix missing attribute** - Add `[Theory]` to `Add_SingleEnumerableParameter_ReturnParametersString`

### Medium Effort

1. **`TwitchHttpClient`** - Mock HttpClient, test rate limiting
2. **`HttpQueryParameters`** - URL encoding edge cases
3. **WebSocket message parsing** - Test without actual connection

### Larger Efforts

1. **Helix request/response serialization** - Ensure JSON round-trips correctly
2. **WebSocket client** - Requires mocking `IWebsocketClient`
3. **Error scenarios** - Test `ApiException` handling

---

## Running Tests

```bash
# Run all unit tests
dotnet test TwitchySharp.Api.Tests.Unit
dotnet test TwitchySharp.Helpers.Tests.Unit
dotnet test TwitchySharp.EventSub.Tests.Unit

# Run with verbosity
dotnet test --verbosity normal

# Run specific test class
dotnet test --filter "FullyQualifiedName~Test_HttpQueryParameters"
```

---

## Contributing Tests

When adding tests:

1. Follow existing naming: `Test_{ClassName}.cs`
2. Use xUnit `[Fact]` for single cases, `[Theory]` with `[InlineData]` for multiple
3. Follow Arrange-Act-Assert pattern
4. Place in corresponding `.Tests.Unit` project
