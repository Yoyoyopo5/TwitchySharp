# TwitchySharp
An up-to-date and easy to use Twitch API wrapper for .NET Core.

## Documentation

- [Architecture Overview](Docs/Architecture.md) - Component diagrams, class hierarchies, and sequence flows
- [Webhook Implementation Plan](Docs/WebhookImplementationPlan.md) - Roadmap for webhooks + ASP.NET Core middleware

## Work In Progress
TwitchySharp is still undergoing initial development. 
Expect breaking changes.

### Completed Features
- [X] Authorization API
- [X] Helix API
- [X] API Integration Tests

## TODO

### API
- [ ] Change duration type properties to use TimeSpan

### EventSub 
- [ ] Notification Models
    - [ ] Automod
    - [ ] Channel
        - [ ] Ad Break
        - [ ] Channel Points
        - [ ] Charity Campaign
        - [ ] Chat
        - [ ] Chat Settings
        - [ ] Goals
        - [ ] Guest Star
        - [ ] Hype Train
        - [ ] Moderator
        - [ ] Polls
        - [ ] Predictions
        - [ ] Shared Chat
        - [ ] Shield Mode
        - [ ] Shoutout
        - [ ] Subscription
        - [ ] Suspicious User
        - [ ] Unban Request
        - [ ] VIPs
        - [ ] Warnings
    - [ ] Conduit
    - [ ] Drops
    - [ ] Extension
    - [ ] Stream
    - [ ] User 
- [X] Websockets Notification Handler
- [X] Webhooks Notification Handler
- [ ] ASP.NET Core Integration

### Documentation
- [ ] API Example Project
- [ ] API Quick Start Guide

## Not Planned (Obsolete)
- PubSub
- IRC Chatbots
- Tags API