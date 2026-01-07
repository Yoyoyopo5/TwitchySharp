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
- [X] API E2E Tests

## TODO

### API
- [ ] Bring API up to date with new changes
- [ ] Change duration type properties to use TimeSpan
- [ ] Replace existing enums with ValueBackedEnums
- [ ] Rework Existing Tests

### EventSub 
- [X] Notification Models
- [X] Websockets Notification Handler
- [X] Webhooks Notification Handler
- [X] ASP.NET Core Integration
- [ ] E2E Tests for Webhooks
- [ ] Manual E2E Testing Pass (to verify event shapes)

### Documentation
- [ ] API Example Project
- [ ] API Quick Start Guide
- [ ] EventSub Websockets Quick Start Guide
- [ ] EventSub Webhooks Quick Start Guide

### Deployment
- [ ] Create Testing Workflow
- [ ] Create Tag Deployment to NuGet Workflow

## Not Planned (Obsolete)
- PubSub
- IRC Chatbots
- Tags API