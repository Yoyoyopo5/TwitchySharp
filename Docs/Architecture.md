# TwitchySharp Architecture

This document provides an overview of the TwitchySharp library architecture using standard software architecture diagrams.

## Table of Contents

- [Solution Structure](#solution-structure)
- [Component Diagram](#component-diagram)
- [Layer Architecture](#layer-architecture)
- [Class Diagrams](#class-diagrams)
  - [API Request Hierarchy](#api-request-hierarchy)
  - [EventSub Components](#eventsub-components)
- [Sequence Diagrams](#sequence-diagrams)
  - [API Request Flow](#api-request-flow)
  - [WebSocket EventSub Flow](#websocket-eventsub-flow)
  - [Webhook EventSub Flow](#webhook-eventsub-flow)
- [Dependency Graph](#dependency-graph)

---

## Solution Structure

Overview of all projects in the solution and their purpose.

```mermaid
graph TB
    subgraph Solution["TwitchySharp.sln"]
        subgraph Core["Core Libraries"]
            API["TwitchySharp.Api<br/>━━━━━━━━━━━━━<br/>Helix & Auth APIs"]
            Shared["TwitchySharp.Shared<br/>━━━━━━━━━━━━━<br/>Common Types & Enums"]
            Helpers["TwitchySharp.Helpers<br/>━━━━━━━━━━━━━<br/>Utilities & Converters"]
        end

        subgraph EventSub["EventSub Libraries"]
            ES["TwitchySharp.EventSub<br/>━━━━━━━━━━━━━<br/>Base EventSub Types"]
            WS["TwitchySharp.EventSub.Websocket<br/>━━━━━━━━━━━━━<br/>WebSocket Client"]
            WH["TwitchySharp.EventSub.Webhooks<br/>━━━━━━━━━━━━━<br/>Webhook Handler"]
        end

        subgraph Tests["Test Projects"]
            T1["Api.Tests.Unit"]
            T2["Api.Tests.Integration"]
            T3["EventSub.Tests.Unit"]
            T4["Websocket.Tests.Integration"]
            T5["Helpers.Tests.Unit"]
        end
    end

    API --> Shared
    API --> Helpers
    ES --> Shared
    WS --> ES
    WH --> ES

    T1 --> API
    T2 --> API
    T3 --> ES
    T4 --> WS
    T5 --> Helpers

    style Core fill:#e1f5fe
    style EventSub fill:#fff3e0
    style Tests fill:#f3e5f5
```

---

## Component Diagram

High-level view of how TwitchySharp interacts with external systems.

```mermaid
graph LR
    subgraph Client["Your Application"]
        App["Application Code"]
    end

    subgraph TwitchySharp["TwitchySharp Library"]
        direction TB
        TC["TwitchHttpClient"]
        WSC["WebSocket Client"]
        WHH["Webhook Handler"]
    end

    subgraph Twitch["Twitch Services"]
        HelixAPI["Helix API<br/>api.twitch.tv"]
        AuthAPI["Auth API<br/>id.twitch.tv"]
        WSSUB["EventSub WebSocket<br/>eventsub.wss.twitch.tv"]
        WHSUB["EventSub Webhooks<br/>(callback to your server)"]
    end

    App --> TC
    App --> WSC
    App --> WHH

    TC -->|"HTTPS"| HelixAPI
    TC -->|"HTTPS"| AuthAPI
    WSC -->|"WSS"| WSSUB
    WHSUB -->|"HTTPS POST"| WHH

    style TwitchySharp fill:#c8e6c9
    style Twitch fill:#bbdefb
```

---

## Layer Architecture

The library follows a layered architecture pattern.

```mermaid
graph TB
    subgraph Presentation["Consumer Layer"]
        Consumer["Your Application"]
    end

    subgraph Public["Public API Layer"]
        TwitchClient["TwitchHttpClient"]
        EventSubWS["TwitchEventSubWebsocketClient"]
        EventSubWH["EventSubWebhookMessageProcessor"]
    end

    subgraph Business["Business Logic Layer"]
        Requests["Request Classes<br/>(HelixApiRequest, AuthorizationApiRequest)"]
        Converters["Response Converters<br/>(JsonApiConverter, EmptyApiConverter)"]
        Handlers["Event Handlers<br/>(IWebsocketEventSubHandler)"]
    end

    subgraph Domain["Domain Layer"]
        Models["Response Models<br/>(Records & DTOs)"]
        Notifications["EventSub Notifications"]
        Enums["Enums & Constants"]
    end

    subgraph Infrastructure["Infrastructure Layer"]
        Http["HttpClient"]
        WebSocket["WebsocketClient"]
        Json["System.Text.Json"]
        RateLimiter["RateLimiter"]
    end

    Consumer --> Public
    Public --> Business
    Business --> Domain
    Business --> Infrastructure

    style Presentation fill:#ffecb3
    style Public fill:#c8e6c9
    style Business fill:#bbdefb
    style Domain fill:#e1bee7
    style Infrastructure fill:#ffccbc
```

---

## Class Diagrams

### API Request Hierarchy

The request class hierarchy enables type-safe API calls with built-in authentication.

```mermaid
classDiagram
    class TwitchApiRequest~TResponse~ {
        <<abstract>>
        +string Endpoint
        +HttpMethod Method
        +HttpContent? Content
        +GetResponseConverter() IConvertApiResponse~TResponse~
    }

    class AuthorizationApiRequest~TResponse~ {
        <<abstract>>
        #string ClientId
        #string ClientSecret
    }

    class HelixApiRequest~TResponse~ {
        <<abstract>>
        #string ClientId
        #string AccessToken
        +GetRequestMessage() HttpRequestMessage
    }

    class IConvertApiResponse~TResponse~ {
        <<interface>>
        +ConvertAsync(HttpResponseMessage) ValueTask~TResponse~
    }

    class JsonApiResponseConverter~TResponse~ {
        +ConvertAsync(HttpResponseMessage) ValueTask~TResponse~
    }

    class EmptyApiResponseConverter {
        +ConvertAsync(HttpResponseMessage) ValueTask~EmptyApiResponse~
    }

    TwitchApiRequest <|-- AuthorizationApiRequest
    TwitchApiRequest <|-- HelixApiRequest
    TwitchApiRequest ..> IConvertApiResponse : creates
    IConvertApiResponse <|.. JsonApiResponseConverter
    IConvertApiResponse <|.. EmptyApiResponseConverter

    class GetChannelInfoRequest {
        +broadcasterIds: IEnumerable~string~
    }

    class GetUsersRequest {
        +ids: IEnumerable~string~
        +logins: IEnumerable~string~
    }

    HelixApiRequest <|-- GetChannelInfoRequest
    HelixApiRequest <|-- GetUsersRequest
```

### EventSub Components

Components involved in EventSub real-time notifications.

```mermaid
classDiagram
    class TwitchEventSubWebsocketClient {
        -IWebsocketClient _ws
        -IWebsocketEventSubHandler _handler
        +StartAsync() Task
        +StopAsync() Task
        +ProcessMessage(string) ValueTask
    }

    class IWebsocketEventSubHandler {
        <<interface>>
        +HandleWelcomeAsync(WebsocketWelcome) ValueTask
        +HandleKeepAliveAsync() ValueTask
        +HandleNotificationAsync(EventSubNotification) ValueTask
        +HandleReconnectAsync(WebsocketReconnect) ValueTask
        +HandleRevocationAsync() ValueTask
    }

    class IEventSubNotification {
        <<interface>>
        +Subscription: EventSubSubscription
    }

    class EventSubSubscription {
        +Id: string
        +Type: string
        +Version: string
        +Status: string
        +Condition: Dictionary
    }

    class ChannelChatMessageNotification {
        +BroadcasterUserId: string
        +ChatterUserId: string
        +Message: ChatMessage
    }

    class AutomodMessageHoldNotification {
        +BroadcasterUserId: string
        +UserId: string
        +Message: string
    }

    TwitchEventSubWebsocketClient --> IWebsocketEventSubHandler
    TwitchEventSubWebsocketClient ..> IEventSubNotification : processes
    IEventSubNotification <|.. ChannelChatMessageNotification
    IEventSubNotification <|.. AutomodMessageHoldNotification
    IEventSubNotification --> EventSubSubscription
```

---

## Sequence Diagrams

### API Request Flow

How a typical Helix API request flows through the system.

```mermaid
sequenceDiagram
    participant App as Application
    participant Client as TwitchHttpClient
    participant Request as HelixApiRequest
    participant RL as RateLimiter
    participant HTTP as HttpClient
    participant Twitch as Twitch API
    participant Conv as ResponseConverter

    App->>Client: SendRequestAsync(request)
    Client->>Request: GetRequestMessage()
    Request-->>Client: HttpRequestMessage

    Client->>RL: AcquireAsync()

    alt Rate Limit Available
        RL-->>Client: Lease Acquired
        Client->>HTTP: SendAsync(message)
        HTTP->>Twitch: HTTPS Request
        Twitch-->>HTTP: HTTP Response
        HTTP-->>Client: HttpResponseMessage

        Client->>Request: GetResponseConverter()
        Request-->>Client: IConvertApiResponse
        Client->>Conv: ConvertAsync(response)
        Conv-->>Client: TResponse
        Client-->>App: TResponse
    else Rate Limited
        RL-->>Client: OperationCanceledException
        Client-->>App: Exception
    end
```

### WebSocket EventSub Flow

Real-time event subscription via WebSocket.

```mermaid
sequenceDiagram
    participant App as Application
    participant Client as EventSubWebsocketClient
    participant Handler as IWebsocketEventSubHandler
    participant WS as WebsocketClient
    participant Twitch as Twitch EventSub

    App->>Client: StartAsync()
    Client->>WS: Start()
    WS->>Twitch: Connect WSS
    Twitch-->>WS: Connected

    Twitch->>WS: Welcome Message
    WS->>Client: OnMessageReceived
    Client->>Client: ProcessMessage()
    Client->>Handler: HandleWelcomeAsync(sessionId)
    Note over App,Handler: App uses sessionId to create subscriptions via API

    loop Keep Alive
        Twitch->>WS: Keepalive Message
        WS->>Client: OnMessageReceived
        Client->>Handler: HandleKeepAliveAsync()
    end

    Twitch->>WS: Notification Message
    WS->>Client: OnMessageReceived
    Client->>Client: Deserialize Notification
    Client->>Handler: HandleNotificationAsync(notification)
    Handler->>App: Process Event

    alt Reconnect Required
        Twitch->>WS: Reconnect Message
        WS->>Client: OnMessageReceived
        Client->>Handler: HandleReconnectAsync(newUrl)
        Client->>WS: Reconnect to new URL
    end
```

### Webhook EventSub Flow

Server-to-server event delivery via webhooks (planned implementation).

```mermaid
sequenceDiagram
    participant Twitch as Twitch EventSub
    participant Server as Your Server
    participant Processor as WebhookMessageProcessor
    participant Handler as Event Handler

    Note over Twitch,Server: Subscription Setup
    Server->>Twitch: Create Subscription (callback URL)
    Twitch->>Server: Challenge Request
    Server->>Processor: HandleRequest()
    Processor->>Processor: Verify Challenge
    Processor-->>Twitch: Echo Challenge
    Twitch-->>Server: Subscription Active

    Note over Twitch,Server: Event Delivery
    Twitch->>Server: POST /webhook (signed)
    Server->>Processor: HandleRequest()
    Processor->>Processor: Verify HMAC Signature

    alt Valid Signature
        Processor->>Processor: Deserialize Notification
        Processor->>Handler: HandleNotification()
        Handler->>Handler: Process Event
        Processor-->>Twitch: 200 OK
    else Invalid Signature
        Processor-->>Twitch: 403 Forbidden
    end
```

---

## Dependency Graph

NuGet package dependencies for each project.

```mermaid
graph TB
    subgraph External["External Dependencies"]
        STJ["System.Text.Json"]
        JWT["Microsoft.IdentityModel<br/>.JsonWebTokens"]
        RL["System.Threading<br/>.RateLimiting"]
        WSC["Websocket.Client"]
        MEH["Microsoft.Extensions<br/>.Hosting.Abstractions"]
    end

    subgraph Projects["TwitchySharp Projects"]
        API["TwitchySharp.Api"]
        Helpers["TwitchySharp.Helpers"]
        Shared["TwitchySharp.Shared"]
        ES["TwitchySharp.EventSub"]
        WS["TwitchySharp.EventSub<br/>.Websocket"]
        WH["TwitchySharp.EventSub<br/>.Webhooks"]
    end

    API --> STJ
    API --> JWT
    API --> RL
    API --> Helpers
    API --> Shared

    Helpers --> STJ
    Shared --> STJ

    ES --> Shared
    ES --> STJ

    WS --> ES
    WS --> WSC
    WS --> MEH

    WH --> ES

    style External fill:#ffccbc
    style Projects fill:#c8e6c9
```

---

## Key Design Patterns

| Pattern | Usage | Location |
|---------|-------|----------|
| **Strategy** | Response converters for different response types | `IConvertApiResponse<T>` |
| **Template Method** | Base request classes define structure | `TwitchApiRequest<T>` |
| **Factory** | Converter creation via attributes | `ApiConverterAttribute` |
| **Observer** | Event handling in WebSocket client | `IWebsocketEventSubHandler` |
| **Adapter** | Wrapping external WebSocket library | `TwitchEventSubWebsocketClient` |

---

## Technology Stack

| Component | Technology |
|-----------|------------|
| **Framework** | .NET 8.0 |
| **Language** | C# 12 (latest) |
| **Serialization** | System.Text.Json |
| **HTTP Client** | HttpClient (built-in) |
| **WebSocket** | Websocket.Client |
| **Rate Limiting** | System.Threading.RateLimiting |
| **JWT Handling** | Microsoft.IdentityModel.JsonWebTokens |
| **Testing** | xUnit |
