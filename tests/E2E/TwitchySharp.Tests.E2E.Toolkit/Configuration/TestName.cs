using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Tests.E2E;

[Wrapper<string>]
public readonly partial record struct TestName(string Value);
