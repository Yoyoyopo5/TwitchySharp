using System.Text.Json;
using System.Text.Json.Nodes;
using TwitchySharp.Api.Helix.Extensions;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Tests.Unit.Helix.Extensions;

public class Test_SendExtensionPubSubMessageRequestData
{
    private static readonly JsonSerializerOptions JsonOptions = JsonConfig.ApiOptions;

    [Fact]
    public void GlobalPubSubMessageData_IsGlobalBroadcast_IsTrue()
    {
        GlobalPubSubMessageData data = new() { Message = "test message" };

        Assert.True(data.IsGlobalBroadcast);
    }

    [Fact]
    public void GlobalPubSubMessageData_Target_ContainsGlobal()
    {
        GlobalPubSubMessageData data = new() { Message = "test message" };

        Assert.Contains(ExtensionPubSubMessageTarget.Global, data.Target);
    }

    [Fact]
    public void GlobalPubSubMessageData_Serialize_HasCorrectStructure()
    {
        GlobalPubSubMessageData data = new() { Message = "test message" };

        string json = JsonSerializer.Serialize(data, JsonOptions);
        JsonNode? jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        Assert.True(jsonNode["is_global_broadcast"]?.GetValue<bool>());
        Assert.Equal("test message", jsonNode["message"]?.GetValue<string>());
        Assert.Contains("global", jsonNode["target"]?.AsArray().Select(n => n?.GetValue<string>()) ?? []);
    }

    [Fact]
    public void BroadcastPubSubMessageData_IsGlobalBroadcast_IsFalse()
    {
        BroadcastPubSubMessageData data = new() { Message = "test message" };

        Assert.False(data.IsGlobalBroadcast);
    }

    [Fact]
    public void BroadcastPubSubMessageData_To_AddsBroadcastTarget()
    {
        BroadcastPubSubMessageData data = new BroadcastPubSubMessageData() { Message = "test message" }
            .To(new UserId("broadcaster123"));

        Assert.Contains(ExtensionPubSubMessageTarget.Broadcast, data.Target);
        Assert.Equal(new UserId("broadcaster123"), data.BroadcasterId);
    }

    [Fact]
    public void BroadcastPubSubMessageData_WhisperTo_AddsWhisperTarget()
    {
        BroadcastPubSubMessageData data = new BroadcastPubSubMessageData { Message = "test message" }
            .WhisperTo(new UserId("user456"));

        ExtensionPubSubMessageTarget whisperTarget = data.Target.FirstOrDefault(t => t.Value.StartsWith("whisper-"));

        Assert.Equal("whisper-user456", whisperTarget.Value);
    }

    [Fact]
    public void BroadcastPubSubMessageData_ChainedToAndWhisperTo_AccumulatesTargets()
    {
        BroadcastPubSubMessageData data = new BroadcastPubSubMessageData { Message = "test message" }
            .To(new UserId("broadcaster123"))
            .To(new UserId("broadcaster456"))
            .WhisperTo(new UserId("user123"))
            .WhisperTo(new UserId("user456"));

        Assert.Equal(4, data.Target.Count());
        Assert.Contains(ExtensionPubSubMessageTarget.Broadcast, data.Target);
        Assert.Contains(data.Target, t => t.Value == "broadcaster123");
        Assert.Contains(data.Target, t => t.Value == "broadcaster456");
        Assert.Contains(data.Target, t => t.Value == "whisper-user123");
        Assert.Contains(data.Target, t => t.Value == "whisper-user456");
    }

    [Fact]
    public void BroadcastPubSubMessageData_To_SerializesWithCorrectStructure()
    {
        BroadcastPubSubMessageData data = new BroadcastPubSubMessageData { Message = "test message" }
            .To(new UserId("broadcaster123"));

        string json = JsonSerializer.Serialize(data, JsonOptions);
        JsonNode? jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        Assert.False(jsonNode["is_global_broadcast"]?.GetValue<bool>());
        Assert.Equal("broadcaster123", jsonNode["broadcaster_id"]?.GetValue<string>());
        Assert.Equal("test message", jsonNode["message"]?.GetValue<string>());
        Assert.Contains("broadcast", jsonNode["target"]?.AsArray().Select(n => n?.GetValue<string>()) ?? []);
    }

    [Fact]
    public void BroadcastPubSubMessageData_OriginalInstance_IsUnmodified()
    {
        BroadcastPubSubMessageData original = new() { Message = "test message" };
        _ = original.To(new UserId("broadcaster123"));

        Assert.Empty(original.Target);
        Assert.Null(original.BroadcasterId);
    }

    [Fact]
    public void ExtensionPubSubMessageTarget_Whisper_HasCorrectFormat()
    {
        ExtensionPubSubMessageTarget whisper = ExtensionPubSubMessageTarget.Whisper("user123");

        Assert.Equal("whisper-user123", whisper.Value);
    }
}
