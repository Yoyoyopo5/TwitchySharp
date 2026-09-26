using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Nodes;
using TwitchySharp.Api.Helix.Users;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Tests.Unit.Helix.Users;

public class Test_UpdateUserExtensionsRequest
{
    private const string FAKE_EXTENSION_ID = "rh6jq1q334hqc2rr1qlzqbvwlfl3x0";
    private const string FAKE_EXTENSION_VERSION = "1.1.0";
    private const int COMPONENT_X = 0;
    private const int COMPONENT_Y = 0;

    private static readonly JsonSerializerOptions JsonOptions = JsonConfig.ApiOptions;

    private static readonly UpdateExtensionParameters FakeExtensionParameters = new()
    {
        Id = new ExtensionId(FAKE_EXTENSION_ID),
        Version = new ExtensionVersion(FAKE_EXTENSION_VERSION),
        Active = true
    };

    private static readonly UpdateComponentExtensionParameters FakeComponentExtensionParameters = new()
    {
        Id = new ExtensionId(FAKE_EXTENSION_ID),
        Version = new ExtensionVersion(FAKE_EXTENSION_VERSION),
        Active = true,
        X = COMPONENT_X,
        Y = COMPONENT_Y
    };

    [Fact]
    public void Serialize_PanelExtensions_ProducesCorrectJsonStructure()
    {
        ExtensionsConfiguration fakeConfig = new()
        {
            PanelExtensions = [FakeExtensionParameters, null, null]
        };
        UpdateUserExtensionsRequestData requestData = new(fakeConfig);

        string json = JsonSerializer.Serialize(requestData, JsonOptions);
        JsonNode? jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        JsonNode? panelNode = jsonNode["data"]?["panel"]?["1"];
        Assert.NotNull(panelNode);
        Assert.True(panelNode["active"]?.GetValue<bool>());
        Assert.Equal(FAKE_EXTENSION_ID, panelNode["id"]?.GetValue<string>());
        Assert.Equal(FAKE_EXTENSION_VERSION, panelNode["version"]?.GetValue<string>());
    }

    [Fact]
    public void Serialize_MultiplePanelExtensions_UsesCorrect1BasedKeys()
    {
        ExtensionsConfiguration config = new()
        {
            PanelExtensions = [
                FakeExtensionParameters,
                null,
                FakeExtensionParameters
            ]
        };
        UpdateUserExtensionsRequestData requestData = new(config);

        string json = JsonSerializer.Serialize(requestData, JsonOptions);
        JsonNode? jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        JsonNode? panelNode = jsonNode["data"]?["panel"];
        Assert.NotNull(panelNode);
        Assert.NotNull(panelNode["1"]);
        Assert.NotNull(panelNode["3"]);
    }

    [Fact]
    public void Serialize_OverlayExtensions_ProducesCorrectJsonStructure()
    {
        ExtensionsConfiguration config = new()
        {
            OverlayExtensions = [FakeExtensionParameters]
        };
        UpdateUserExtensionsRequestData requestData = new(config);

        string json = JsonSerializer.Serialize(requestData, JsonOptions);
        JsonNode? jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        JsonNode? overlayNode = jsonNode["data"]?["overlay"]?["1"];
        Assert.NotNull(overlayNode);
        Assert.True(overlayNode["active"]?.GetValue<bool>());
        Assert.Equal(FAKE_EXTENSION_ID, overlayNode["id"]?.GetValue<string>());
        Assert.Equal(FAKE_EXTENSION_VERSION, overlayNode["version"]?.GetValue<string>());
    }

    [Fact]
    public void Serialize_ComponentExtensions_IncludesXYCoordinates()
    {
        ExtensionsConfiguration config = new()
        {
            ComponentExtensions = [FakeComponentExtensionParameters, null]
        };
        UpdateUserExtensionsRequestData requestData = new(config);

        string json = JsonSerializer.Serialize(requestData, JsonOptions);
        JsonNode? jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        JsonNode? componentNode = jsonNode["data"]?["component"]?["1"];
        Assert.NotNull(componentNode);
        Assert.True(componentNode["active"]?.GetValue<bool>());
        Assert.Equal(FAKE_EXTENSION_ID, componentNode["id"]?.GetValue<string>());
        Assert.Equal(FAKE_EXTENSION_VERSION, componentNode["version"]?.GetValue<string>());
        Assert.Equal(COMPONENT_X, componentNode["x"]?.GetValue<int>());
        Assert.Equal(COMPONENT_Y, componentNode["y"]?.GetValue<int>());
    }

    [Fact]
    public void Serialize_MixedExtensionTypes_ProducesCorrectJsonStructure()
    {
        ExtensionsConfiguration config = new()
        {
            PanelExtensions = [FakeExtensionParameters, null , null],
            OverlayExtensions = [FakeExtensionParameters],
            ComponentExtensions = [FakeComponentExtensionParameters, null]
        };
        UpdateUserExtensionsRequestData requestData = new(config);

        string json = JsonSerializer.Serialize(requestData, JsonOptions);
        JsonNode? jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        Assert.NotNull(jsonNode["data"]?["panel"]?["1"]);
        Assert.NotNull(jsonNode["data"]?["overlay"]?["1"]);
        Assert.NotNull(jsonNode["data"]?["component"]?["1"]);
    }

    [Fact]
    public void Serialize_EmptyConfiguration_OmitsExtensionTypes()
    {
        ExtensionsConfiguration config = new();
        UpdateUserExtensionsRequestData requestData = new(config);

        string json = JsonSerializer.Serialize(requestData, JsonOptions);
        JsonNode? jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        JsonObject? dataNode = jsonNode["data"]?.AsObject();
        Assert.NotNull(dataNode);
        Assert.False(dataNode.ContainsKey("panel"));
        Assert.False(dataNode.ContainsKey("overlay"));
        Assert.False(dataNode.ContainsKey("component"));
    }

    [Fact]
    public void Deserialize_Response_ParsesCorrectly()
    {
        const string responseJson = """
        {
          "data": {
            "panel": {
              "1": {
                "active": true,
                "id": "rh6jq1q334hqc2rr1qlzqbvwlfl3x0",
                "version": "1.1.0",
                "name": "TopClip"
              }
            },
            "overlay": {
              "1": {
                "active": true,
                "id": "zfh2irvx2jb4s60f02jq0ajm8vwgka",
                "version": "1.0.19",
                "name": "Streamlabs"
              }
            },
            "component": {
              "1": {
                "active": true,
                "id": "lqnf3zxk0rv0g7gq92mtmnirjz2cjj",
                "version": "0.0.1",
                "name": "Dev Experience Test",
                "x": 0,
                "y": 0
              },
              "2": {
                "active": false
              }
            }
          }
        }
        """;

        UpdateUserExtensionsResponseContent? response = JsonSerializer.Deserialize<UpdateUserExtensionsResponseContent>(responseJson, JsonOptions);

        Assert.NotNull(response);
        Assert.NotNull(response.Data);

        UserActiveExtension panel1 = response.Data.Panel["1"];
        Assert.True(panel1.Active);
        Assert.NotNull(panel1.Id);
        Assert.Equal("rh6jq1q334hqc2rr1qlzqbvwlfl3x0", panel1.Id!.Value.Value);

        Assert.True(response.Data.Overlay["1"].Active);
        Assert.True(response.Data.Component["1"].Active);
        Assert.Equal(0, response.Data.Component["1"].X);
        Assert.Equal(0, response.Data.Component["1"].Y);
        Assert.False(response.Data.Component["2"].Active);
    }
}
