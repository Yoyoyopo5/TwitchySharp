# TwitchySharp

A strongly-typed .NET 8.0 wrapper around the public Twitch API. The goal is to provide strongly-typed requests, responses, and real-time notifications while simplifying request authorization handling.

- **Framework**: .NET 8.0, C# latest (C#14)
- **License**: MIT
- **Status**: Work in progress, expect breaking changes

## Solution Structure

The solution has two main branches and several supporting projects.

### Main Branches

**TwitchySharp.Api** — Twitch REST API (Authorization + Helix).
- Authorization API endpoints generally do not require `ClientId`/`Authorization` headers.
- Helix API endpoints almost always require both.
- Requests inherit from `AuthorizationApiRequest<T>` or `HelixApiRequest<T>`, both extending `TwitchApiRequest<T>`.

**TwitchySharp.EventSub** — Twitch subscription API for real-time data via webhooks or websockets.

### Supporting Projects

| Project | Purpose |
|---|---|
| `TwitchySharp.Shared` | Shared data shapes between Api and EventSub |
| `TwitchySharp.Helpers` | Decoupled helper classes (JSON converters, query builders) with no Twitch API references |
| `TwitchySharp.Helpers.Functional` | Functional pipeline library (Steps, Layers, Effects) |
| `TwitchySharp.Api.AuthorizationResolution` | Resolves `ClientId` and `Authorization` header values for Helix requests |
| `TwitchySharp.EventSub.Webhooks` | Webhook-specific EventSub notification processing |
| `TwitchySharp.EventSub.Websocket` | Websocket-specific EventSub notification processing |
| `TwitchySharp.EventSub.Webhooks.AspNetCore` | ASP.NET Core integration for webhooks |
| `TwitchySharp.EventSub.Websocket.Clients.WebsocketClient` | Websocket client implementation |

### Test Projects

Named `TwitchySharp.[PROJECT].Tests.[TEST_TYPE]` (e.g. `TwitchySharp.Api.Tests.Unit`, `TwitchySharp.EventSub.Websocket.Tests.E2E`). Test types: `Unit`, `Integration`, `E2E`.

## Conventions

### Namespaces
Namespaces are decoupled from folder structure. Folders organize internally; namespaces are kept broad and useful for end-developers consuming the library.

### Code Style
- Expression-bodied members wherever possible
- Switch expressions over switch statements
- Guard clauses and early returns over `else`
- No `var` keyword — use explicit types
- Collection expressions (`[1, 2, 3]`) over old initializer syntax
- Primary constructors with `private readonly` field assignment
- Single-line `if` statements without braces
- LINQ over imperative foreach loops
- Minimal public API surface — prefer `internal` unless truly needed

### Architecture Patterns
- Records for responses (immutability, value equality)
- Primary constructors for request classes
- Abstract base classes over interfaces for shared implementation
- `snake_case` JSON to match Twitch API conventions
- Dependencies captured via closures in functional pipelines (not constructor injection on delegates)

## Build and Test

```
dotnet build TwitchySharp.sln
dotnet test TwitchySharp.sln
```

## Key Documentation

- [Architecture Overview](Docs/Architecture.md) — Component diagrams, class hierarchies, sequence flows
- [Functional Library README](TwitchySharp.Helpers.Functional/README.md) — Steps, Layers, Effects pipeline guide
