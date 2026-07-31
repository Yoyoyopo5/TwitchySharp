# TwitchySharp
A third-party Twitch API/EventSub wrapper for .NET Core.

## Work In Progress
TwitchySharp is still undergoing initial development. 
Expect breaking changes.

### Completed Features
- [X] Api
  - [X] Strongly-typed Twitch Authentication and Helix API endpoints (up-to-date as of February 2026)
  - [X] Authentication header middleware `.WithAuthentication()`
  - [X] Rate limiter middleware `.WithRateLimiting()`
- [X] EventSub
  - [X] Webhooks transport support
    - [X] ASP.NET Core integration `.AddTwitchEventSubWebhooks()`
  - [X] Websockets transport support (Websocket client agnostic)

### TODO

#### API
- [ ] Microsoft.Extensions.Hosting integration (dependency injection)
  
#### EventSub
- [ ] Comprehensive EventSub E2E testing for each subscription type

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
