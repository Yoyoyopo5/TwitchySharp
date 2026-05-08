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
        var data = new GlobalPubSubMessageData { Message = "test message" };

        Assert.True(data.IsGlobalBroadcast);
    }

    [Fact]
    public void GlobalPubSubMessageData_Target_ContainsGlobal()
    {
        var data = new GlobalPubSubMessageData { Message = "test message" };

        Assert.Contains(ExtensionPubSubMessageTarget.Global, data.Target);
    }

    [Fact]
    public void GlobalPubSubMessageData_Serialize_HasCorrectStructure()
    {
        var data = new GlobalPubSubMessageData { Message = "test message" };

        var json = JsonSerializer.Serialize(data, JsonOptions);
        var jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        Assert.True(jsonNode["is_global_broadcast"]?.GetValue<bool>());
        Assert.Equal("test message", jsonNode["message"]?.GetValue<string>());
        Assert.Contains("global", jsonNode["target"]?.AsArray().Select(n => n?.GetValue<string>()) ?? []);
    }

    [Fact]
    public void BroadcastPubSubMessageData_IsGlobalBroadcast_IsFalse()
    {
        var data = new BroadcastPubSubMessageData { Message = "test message" };

        Assert.False(data.IsGlobalBroadcast);
    }

    [Fact]
    public void BroadcastPubSubMessageData_To_AddsBroadcastTarget()
    {
        var data = new BroadcastPubSubMessageData { Message = "test message" }
            .To(new UserId("broadcaster123"));

        Assert.Contains(ExtensionPubSubMessageTarget.Broadcast, data.Target);
        Assert.Equal(new UserId("broadcaster123"), data.BroadcasterId);
    }

    [Fact]
    public void BroadcastPubSubMessageData_WhisperTo_AddsWhisperTarget()
    {
        var data = new BroadcastPubSubMessageData { Message = "test message" }
            .WhisperTo(new UserId("user456"));

        var whisperTarget = data.Target.FirstOrDefault(t => t.Value.StartsWith("whisper-"));

        Assert.Equal("whisper-user456", whisperTarget.Value);
    }

    [Fact]
    public void BroadcastPubSubMessageData_ChainedToAndWhisperTo_AccumulatesTargets()
    {
        var data = new BroadcastPubSubMessageData { Message = "test message" }
            .To(new UserId("broadcaster123"))
            .WhisperTo(new UserId("user456"));

        Assert.Equal(2, data.Target.Count());
        Assert.Contains(ExtensionPubSubMessageTarget.Broadcast, data.Target);
        Assert.Contains(data.Target, t => t.Value == "whisper-user456");
    }

    [Fact]
    public void BroadcastPubSubMessageData_MultipleWhisperTo_AccumulatesWhisperTargets()
    {
        var data = new BroadcastPubSubMessageData { Message = "test message" }
            .WhisperTo(new UserId("user1"))
            .WhisperTo(new UserId("user2"))
            .WhisperTo(new UserId("user3"));

        Assert.Equal(3, data.Target.Count());
        Assert.Contains(data.Target, t => t.Value == "whisper-user1");
        Assert.Contains(data.Target, t => t.Value == "whisper-user2");
        Assert.Contains(data.Target, t => t.Value == "whisper-user3");
    }

    [Fact]
    public void BroadcastPubSubMessageData_To_SerializesWithCorrectStructure()
    {
        var data = new BroadcastPubSubMessageData { Message = "test message" }
            .To(new UserId("broadcaster123"));

        var json = JsonSerializer.Serialize(data, JsonOptions);
        var jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        Assert.False(jsonNode["is_global_broadcast"]?.GetValue<bool>());
        Assert.Equal("broadcaster123", jsonNode["broadcaster_id"]?.GetValue<string>());
        Assert.Equal("test message", jsonNode["message"]?.GetValue<string>());
        Assert.Contains("broadcast", jsonNode["target"]?.AsArray().Select(n => n?.GetValue<string>()) ?? []);
    }

    [Fact]
    public void BroadcastPubSubMessageData_OriginalInstance_IsUnmodified()
    {
        var original = new BroadcastPubSubMessageData { Message = "test message" };
        _ = original.To(new UserId("broadcaster123"));

        Assert.Empty(original.Target);
        Assert.Null(original.BroadcasterId);
    }

    [Fact]
    public void ExtensionPubSubMessageTarget_Whisper_HasCorrectFormat()
    {
        var whisper = ExtensionPubSubMessageTarget.Whisper("user123");

        Assert.Equal("whisper-user123", whisper.Value);
    }
}
