# AuthorizationResolution Functional Refactor Tasklist

Refactor plan for `TwitchySharp.Api.AuthorizationResolution` to eliminate redundant infrastructure by adopting `TwitchySharp.Helpers.Functional` and removing duck-typing interfaces in favor of record inheritance.

---

## Phase 1: Remove Duck-Typing Interfaces

These interfaces exist only to erase generic parameters via explicit interface implementation. The code never uses them polymorphically — it always pattern matches back to concrete types.

### 1.1 Remove `IAccessTokenKey`

**Current state:**
- `IAccessTokenKey` exposes `TwitchApiIdentity Identity`
- `AccessTokenKey<TIdentity>` implements it with explicit bridge: `TwitchApiIdentity IAccessTokenKey.Identity => Identity;`
- `UserAccessTokenKey : AccessTokenKey<UserIdentity>` inherits the implementation
- Used as generic constraint: `where TKey : IAccessTokenKey`
- Pattern matched on concrete types everywhere (never consumed through the interface)

**Action:**
- [X] Remove `IAccessTokenKey` interface
- [X] Remove explicit interface implementation from `AccessTokenKey<TIdentity>`
- [X] Replace all `where TKey : IAccessTokenKey` constraints with direct record type constraints or remove where unnecessary
- [ ] Verify all pattern matching on `AccessTokenKey<T>` / `UserAccessTokenKey` still works
- [ ] Update `IdentityTypeTokenResolver` parameter types

### 1.2 Remove `IAccessTokenDetails`

**Current state:**
- `IAccessTokenDetails` exposes `Identity`, `AccessToken`, `ExpiresAt`
- `AccessTokenDetails<TIdentity, TToken>` implements it with explicit bridges
- Concrete types: `AppAccessTokenDetails`, `UserAccessTokenDetails`, `ExtensionJwtDetails`
- Used as generic constraint: `where TDetails : IAccessTokenDetails`
- Never consumed through the interface — always through concrete generic record

**Action:**
- [ ] Remove `IAccessTokenDetails` interface
- [ ] Remove explicit interface implementation from `AccessTokenDetails<TIdentity, TToken>`
- [ ] Replace all `where TDetails : IAccessTokenDetails` constraints with `where TDetails : AccessTokenDetails<TIdentity, TToken>` or a non-generic base record
- [ ] Update `IHaveAccessTokenDetails<TDetails>` constraint (this interface stays — it's a legitimate marker)
- [ ] Update `IRefreshAccessToken<TDetails>` constraint
- [ ] Update `IStoreAccessTokens<TToken, TKey, TDetails>` constraint

### 1.3 Verify Preserved Interfaces

These interfaces are legitimate and must NOT be removed:

| Interface | Reason |
|---|---|
| `IHaveAccessTokenDetails<TDetails>` | Marker interface — discriminates result types carrying token details from `Unavailable`/`NotRequired` |
| `IStoreAccessTokens<TToken, TKey, TDetails>` | Storage abstraction — multiple implementations expected (in-memory, SQL, Redis) |
| `IStoreUserAccessTokens` | Convenience alias for DI registration |
| `IRefreshAccessToken<TDetails>` | Strategy pattern — users provide custom refresh logic |
| `IResolveClientSecret` | External DI contract for secret resolution |
| `IRequireAuthorization` | External contract from `TwitchySharp.Api` — not ours to change |

---

## Phase 2: Replace Resolver Infrastructure with Functional Library

The project has ~6 files of generic resolver infrastructure that duplicate what `TwitchySharp.Helpers.Functional` already provides. Each resolver is essentially a `Step` or `Layer`.

### 2.1 Remove `IResolveAsync<TKey, TResult>`

**Current state:**
- Base interface: `ValueTask<TResult> ResolveAsync(TKey key, CancellationToken ct)`
- Every resolver in the project implements this
- Equivalent to `Step<TKey, TResult>` (both are `TKey → ValueTask<TResult>`)

**Action:**
- [ ] Remove `IResolveAsync<TKey, TResult>` interface
- [ ] Replace usages with `Step<TKey, TResult>` from the Functional library
- [ ] Note: `CancellationToken` is not part of `Step`'s signature — capture it via closure at pipeline construction time, or evaluate whether a `Step<TKey, TResult>` variant with `CancellationToken` is needed in the Functional library

**Decision needed:** How to handle `CancellationToken`. Options:
  - (a) Capture `CancellationToken` in closure when constructing the pipeline
  - (b) Use `Step<(TKey, CancellationToken), TResult>` tuple input
  - (c) Add a `CancellableStep<TKey, TResult>` delegate to the Functional library

### 2.2 Remove `DelegatingResolver` (both variants)

**Current state:**
- Simple variant: wraps `InnerResolver`, delegates `ResolveAsync` — this is `Layer<TKey, TResult>`
- Generic variant: wraps inner resolver with `MapKey`/`MapResult` transforms — this is two `Step`s sandwiching an inner step

**Action:**
- [ ] Remove `DelegatingResolver<TKey, TResult>` — replace with `Layer<TKey, TResult>`
- [ ] Remove `DelegatingResolver<TKey, TResult, TInnerKey, TInnerResult>` — replace with step composition (`mapKey.Then(innerStep).Then(mapResult)`)
- [ ] Delete `DelegatingResolver.cs`

### 2.3 Remove `ResolverChain` / `AccessTokenResolverChain` / `UserAccessTokenResolverChain`

**Current state:**
- `ResolverChain<TKey, TResult>` — fluent builder that wraps resolvers via `.Then(factory)`, returning new chain instances
- `AccessTokenResolverChain` — typed convenience for access token chains
- `UserAccessTokenResolverChain` — typed convenience for user token chains
- All three replicate what `step.Then(layer)` already does

**Action:**
- [ ] Remove `ResolverChain.cs`
- [ ] Remove `AccessTokenResolverChain.cs`
- [ ] Remove `UserAccessTokenResolverChain.cs`
- [ ] Replace chain building with direct `Step.Then(Layer)` composition

### 2.4 Remove or Replace `SequentialResolver`

**Current state:**
- Iterates through a list of resolvers, returns first non-null result
- Uses C# 12 collection builder syntax
- This is a specific combinator: "try in order, take first success"

**Action:**
- [ ] Evaluate whether to add a `FirstOf` / `Fallback` combinator to the Functional library
- [ ] Or implement as a local factory method that creates a `Step` from a list of steps
- [ ] Remove `SequentialResolver.cs`

### 2.5 Remove `IResolveAccessToken<TKey>` and Specialized Interfaces

**Current state:**
- `IResolveAccessToken<TKey> : IResolveAsync<TKey, AccessTokenResolutionResult>` — just narrows the result type
- `IResolveUserAccessToken`, `IResolveAppAccessToken`, `IResolveExtensionJsonWebToken` — type aliases

**Action:**
- [ ] Remove `IResolveAccessToken<TKey>` — use `Step<TKey, AccessTokenResolutionResult>` directly
- [ ] Remove the three specialized interfaces — they become type aliases or disappear entirely
- [ ] Update `IdentityTypeTokenResolver` constructor parameters to use `Step` types

---

## Phase 3: Convert Concrete Resolvers to Step/Layer Factories

Each existing resolver class becomes a static factory method returning a `Step` or `Layer`.

### 3.1 `ConfiguredAccessTokenResolver` → Step factory

**Current:** Record class implementing `IResolveAsync<IRequireAuthorization, AccessToken?>`
**Becomes:** `Step<IRequireAuthorization, AccessToken?>` — a one-liner that returns `request.OverrideAccessToken`

- [ ] Create static factory method (e.g., in a `TokenResolution` static class)
- [ ] Delete `ConfiguredAccessTokenResolver.cs`

### 3.2 `StoredTokenResolver` → Step factory

**Current:** Record with `IStoreAccessTokens` dependency, maps store result to `AccessTokenResolutionResult`
**Becomes:** `Step<TKey, AccessTokenResolutionResult>` factory that captures the store via closure

- [ ] Create static factory method accepting `IStoreAccessTokens<TToken, TKey, TDetails>`
- [ ] Delete `StoredTokenResolver.cs`

### 3.3 `SingleAccessTokenResolver` → Step factory

**Current:** Record returning a pre-configured token for all requests
**Becomes:** `Step<TKey, AccessTokenResolutionResult>` that always returns the same result

- [ ] Create static factory method accepting the token
- [ ] Delete `SingleAccessTokenResolver.cs`

### 3.4 `RefreshingAccessTokenResolver` → Layer factory

**Current:** `DelegatingResolver` that intercepts `Expired` results and refreshes via `IRefreshAccessToken`
**Becomes:** `Layer<TKey, AccessTokenResolutionResult>` factory that captures the refresher

- [ ] Create static factory method accepting `IRefreshAccessToken<TDetails>`
- [ ] Delete `RefreshingTokenResolver.cs`

### 3.5 `NewTokenWriter` → Layer factory (or Tap)

**Current:** `DelegatingResolver` that intercepts `New` results and saves to store
**Becomes:** `Layer<TKey, AccessTokenResolutionResult>` or `Tap` with `Effect` — observes new tokens and writes to store

- [ ] Decide: Layer (can modify result) vs Tap (observe only). Current impl doesn't modify the result, just saves — Tap is more appropriate
- [ ] Create factory method accepting `IStoreAccessTokens` and optional `ILogger`
- [ ] Delete `NewTokenWriter.cs`

### 3.6 `ConcurrentResolver` → Layer factory

**Current:** `DelegatingResolver` with per-key semaphore locking
**Becomes:** `Layer<TKey, TResult>` factory that captures the lock function and semaphore dictionary

- [ ] Create static factory method accepting `Func<TKey, TLock>` and optional `ILogger`
- [ ] Must be constructed once and reused (contains `ConcurrentDictionary` state)
- [ ] Delete `ConcurrentResolver.cs`

### 3.7 `TokenResolver` → Step factory

**Current:** `DelegatingResolver<IRequireAuthorization, AccessToken?, TKey, AccessTokenResolutionResult>` that maps keys and results between the public API type and internal resolver types
**Becomes:** A `Step` that maps `IRequireAuthorization` → `TKey`, calls the inner pipeline, then maps `AccessTokenResolutionResult` → `AccessToken?`

- [ ] Create static factory method for each identity type (User, App, Extension)
- [ ] Delete `TokenResolver.cs`

### 3.8 `IdentityTypeTokenResolver` → Step factory or keep as record

**Current:** Router that dispatches to identity-specific resolvers based on `TwitchApiIdentity` type
**Becomes:** A `Step<IRequireAuthorization, AccessTokenResolutionResult>` that routes via switch expression

- [ ] Evaluate: keep as record (it has constructor-injected optional dependencies) or convert to factory
- [ ] If factory: accept nullable steps for each identity type, return routing step
- [ ] The routing/dispatch logic is domain-specific and stays regardless of form

---

## Phase 4: Rewrite `DefaultTokenResolver` and `DefaultRequestAuthorizer`

### 4.1 Rewrite `DefaultTokenResolver`

**Current pipeline (built via ResolverChain + SequentialResolver):**
```
SequentialResolver [
    ConfiguredAccessTokenResolver,
    ResolverChain
        .RetrieveFromStore(store)
        .ThenRefreshExpired(refresher)
        .ThenSaveNewTokens(store)
        .ConcurrentlyOn(key => key.Identity)
        .WithIdentity<UserIdentity, UserAccessTokenKey>(mapKey)
]
```

**Target pipeline (using Functional library):**
```csharp
Step<IRequireAuthorization, AccessToken?> pipeline =
    checkConfigured
        .Then(routeByIdentity)    // or fallback combinator
        .Then(mapToAccessToken);

// where routeByIdentity internally dispatches to:
Step<UserAccessTokenKey, AccessTokenResolutionResult> userPipeline =
    retrieveFromStore(store)
        .Then(refreshExpiredLayer(refresher))
        .Tap(saveNewTokensEffect(store))
        .Then(concurrencyLayer(key => key.Identity));
```

- [ ] Rewrite `DefaultTokenResolver` using `Step`/`Layer`/`Tap` composition
- [ ] Verify pipeline behavior matches original (use existing tests)

### 4.2 Simplify `DefaultRequestAuthorizer`

**Current:** Orchestrates client identity resolution + token resolution, creates `AuthorizationRequirement` record for identity fallback.

- [ ] Simplify to compose a single pipeline from client resolution through token resolution
- [ ] Keep `AuthorizationRequirement` helper or inline the identity fallback logic
- [ ] Ensure `IAuthorizeTwitchRequest` contract is preserved (this is the public API)

---

## Phase 5: Update Tests

- [ ] Update unit tests to reflect removed interfaces and new factory-based construction
- [ ] Update integration tests for new pipeline composition
- [ ] Verify all existing test scenarios still pass
- [ ] Add tests for new Functional library combinators if any were added (e.g., `Fallback`/`FirstOf`)

---

## Phase 6: Cleanup

- [ ] Delete all removed files
- [ ] Add project reference to `TwitchySharp.Helpers.Functional` in `.csproj`
- [ ] Remove unused usings across remaining files
- [ ] Final build and test pass

---

## Files Expected to be Deleted

| File | Replaced By |
|---|---|
| `IResolveAsync.cs` | `Step<TKey, TResult>` |
| `DelegatingResolver.cs` | `Layer<TKey, TResult>` |
| `ResolverChain.cs` | `.Then()` chaining |
| `SequentialResolver.cs` | `Fallback` combinator or local factory |
| `AccessTokenResolverChain.cs` | `.Then()` chaining |
| `UserAccessTokenResolverChain.cs` | `.Then()` chaining |
| `ConfiguredAccessTokenResolver.cs` | Step factory method |
| `StoredTokenResolver.cs` | Step factory method |
| `SingleAccessTokenResolver.cs` | Step factory method |
| `RefreshingTokenResolver.cs` | Layer factory method |
| `NewTokenWriter.cs` | Tap/Layer factory method |
| `ConcurrentResolver.cs` | Layer factory method |
| `TokenResolver.cs` | Step factory method |

## Files Expected to be Modified

| File | Change |
|---|---|
| `AccessTokenKey.cs` | Remove `IAccessTokenKey` interface |
| `IResolveAccessToken.cs` | Remove or reduce to type alias |
| `IdentityTypeTokenResolver.cs` | Accept `Step` types instead of resolver interfaces |
| `DefaultTokenResolver.cs` | Rewrite using Functional pipeline |
| `DefaultRequestAuthorizer.cs` | Simplify orchestration |
| `InMemoryUserAccessTokenStore.cs` | No change (storage is preserved) |
| `TwitchUserAccessTokenRefresher.cs` | No change (strategy is preserved) |
| `.csproj` | Add reference to `TwitchySharp.Helpers.Functional` |

## Open Decisions

1. **CancellationToken handling** — How to thread `CancellationToken` through `Step`-based pipelines. Closure capture? Tuple input? New delegate variant?
2. **`Fallback`/`FirstOf` combinator** — Should this be added to the Functional library or kept local to AuthorizationResolution?
3. **`IdentityTypeTokenResolver` form** — Keep as record with constructor DI, or convert to factory method?
4. **`IAccessTokenDetails` removal scope** — Remove entirely, or keep as a non-generic base record?
