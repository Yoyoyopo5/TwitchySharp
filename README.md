# TwitchySharp
A third-party Twitch API wrapper for .NET Core.

## Work In Progress
TwitchySharp is still undergoing initial development. 
Expect breaking changes.

### Completed Features
- [X] Custom `ITwitchApi` typed HttpHandler
  - [X] Strongly-typed Twitch Authentication and Helix API endpoints (up-to-date as of Feburary 2026)
  - [X] Authentication header middleware `.WithTwitchAuthorization()`
  - [X] Rate limiter middleware `.WithRateLimiting()`
- [X] EventSub
  - [X] Webhooks transport support
    - [X] ASP.NET Core integration `.AddTwitchEventSubWebhooks()`
  - [X] Websockets transport support
    - [X] [Websocket.Client](https://github.com/Marfusios/websocket-client) integration

### TODO

#### API
- [ ] Microsoft.Extensions.Hosting integration (dependency injection)
  
#### EventSub
- [ ] Comprehensive E2E Testing

#### Documentation
- [ ] API Example Project
- [ ] API Quick Start Guide
- [ ] EventSub Websockets Quick Start Guide
- [ ] EventSub Webhooks Quick Start Guide

#### Deployment
- [ ] Create Testing Workflow
- [ ] Create Tag Deployment to NuGet Workflow

#### Not Planned (Obsolete)
- PubSub
- IRC Chatbots
- Tags API
