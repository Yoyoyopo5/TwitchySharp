using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Nodes;
using TwitchySharp.Api.Helix.Users;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Tests.Unit.Helix.Users;

public class Test_UpdateUserExtensionsRequest
{
    private static readonly JsonSerializerOptions JsonOptions = JsonConfig.ApiOptions;

    [Fact]
    public void Serialize_PanelExtensions_ProducesCorrectJsonStructure()
    {
        ExtensionsConfiguration config = new()
        {
            PanelExtensions = [new UpdateExtensionParameters()
            {
                Id = new ExtensionId("rh6jq1q334hqc2rr1qlzqbvwlfl3x0"),
                Version = new ExtensionVersion("1.1.0"),
                Active = true
            }]
        };
        UpdateUserExtensionsRequestData requestData = new(config);

        string json = JsonSerializer.Serialize(requestData, JsonOptions);
        JsonNode? jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        JsonNode? panelNode = jsonNode["data"]?["panel"]?["1"];
        Assert.NotNull(panelNode);
        Assert.True(panelNode["active"]?.GetValue<bool>());
        Assert.Equal("rh6jq1q334hqc2rr1qlzqbvwlfl3x0", panelNode["id"]?.GetValue<string>());
        Assert.Equal("1.1.0", panelNode["version"]?.GetValue<string>());
    }

    [Fact]
    public void Serialize_MultiplePanelExtensions_UsesCorrect1BasedKeys()
    {
        ExtensionsConfiguration config = new()
        {
            PanelExtensions = [
                new UpdateExtensionParameters()
                {
                    Id = new ExtensionId("ext1"),
                    Version = new ExtensionVersion("1.0.0"),
                    Active = true
                },
                new UpdateExtensionParameters()
                {
                    Id = new ExtensionId("ext2"),
                    Version = new ExtensionVersion("2.0.0"),
                    Active = false
                }
            ]
        };
        UpdateUserExtensionsRequestData requestData = new(config);

        string json = JsonSerializer.Serialize(requestData, JsonOptions);
        JsonNode? jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        JsonNode? panelNode = jsonNode["data"]?["panel"];
        Assert.NotNull(panelNode);
        Assert.NotNull(panelNode["1"]);
        Assert.NotNull(panelNode["2"]);
    }

    [Fact]
    public void Serialize_OverlayExtensions_ProducesCorrectJsonStructure()
    {
        ExtensionsConfiguration config = new()
        {
            OverlayExtensions = [new UpdateExtensionParameters()
                {
                    Id = new ExtensionId("zfh2irvx2jb4s60f02jq0ajm8vwgka"),
                    Version = new ExtensionVersion("1.0.19"),
                    Active = true
                }]
        };
        UpdateUserExtensionsRequestData requestData = new(config);

        string json = JsonSerializer.Serialize(requestData, JsonOptions);
        JsonNode? jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        JsonNode? overlayNode = jsonNode["data"]?["overlay"]?["1"];
        Assert.NotNull(overlayNode);
        Assert.True(overlayNode["active"]?.GetValue<bool>());
        Assert.Equal("zfh2irvx2jb4s60f02jq0ajm8vwgka", overlayNode["id"]?.GetValue<string>());
        Assert.Equal("1.0.19", overlayNode["version"]?.GetValue<string>());
    }

    [Fact]
    public void Serialize_ComponentExtensions_IncludesXYCoordinates()
    {
        ExtensionsConfiguration config = new()
        {
            ComponentExtensions = [new UpdateComponentExtensionParameters()
            {
                Id = new ExtensionId("lqnf3zxk0rv0g7gq92mtmnirjz2cjj"),
                Version = new ExtensionVersion("0.0.1"),
                Active = true,
                X = 0,
                Y = 0
            }]
        };
        UpdateUserExtensionsRequestData requestData = new(config);

        string json = JsonSerializer.Serialize(requestData, JsonOptions);
        JsonNode? jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        JsonNode? componentNode = jsonNode["data"]?["component"]?["1"];
        Assert.NotNull(componentNode);
        Assert.True(componentNode["active"]?.GetValue<bool>());
        Assert.Equal("lqnf3zxk0rv0g7gq92mtmnirjz2cjj", componentNode["id"]?.GetValue<string>());
        Assert.Equal("0.0.1", componentNode["version"]?.GetValue<string>());
        Assert.Equal(0, componentNode["x"]?.GetValue<int>());
        Assert.Equal(0, componentNode["y"]?.GetValue<int>());
    }

    [Fact]
    public void Serialize_MixedExtensionTypes_ProducesCorrectJsonStructure()
    {
        ExtensionsConfiguration config = new()
        {
            PanelExtensions = [new UpdateExtensionParameters()
                {
                    Id = new ExtensionId("panel_ext"),
                    Version = new ExtensionVersion("1.0.0"),
                    Active = true,
                }],
            OverlayExtensions = [new UpdateExtensionParameters()
                {
                    Id = new ExtensionId("overlay_ext"),
                    Version = new ExtensionVersion("2.0.0"),
                    Active = true
                }],
            ComponentExtensions = [new UpdateComponentExtensionParameters()
                {
                    Id = new ExtensionId("component_ext"),
                    Version = new ExtensionVersion("3.0.0"),
                    Active = true,
                    X = 100,
                    Y = 200,
                }]
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
    public void ExtensionsConfigurationType_ChainedConfigureExtension_AccumulatesExtensions()
    {
        ImmutableArray<UpdateExtensionParameters?> config = [
            new UpdateExtensionParameters()
            {
                Id = new ExtensionId("ext1"),
                Version = new ExtensionVersion("1.0.0"),
                Active = true
            },
            new UpdateExtensionParameters()
            {
                Id = new ExtensionId("ext2"),
                Version = new ExtensionVersion("2.0.0"),
                Active = false
            },
            new UpdateExtensionParameters()
            {
                Id = new ExtensionId("ext3"),
                Version = new ExtensionVersion("3.0.0"),
                Active = true
            },
        ];

        ExtensionsConfiguration fullConfig = new() { PanelExtensions = config };
        UpdateUserExtensionsRequestData requestData = new(fullConfig);
        string json = JsonSerializer.Serialize(requestData, JsonOptions);
        JsonNode? jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode?["data"]?["panel"]?["1"]);
        Assert.NotNull(jsonNode?["data"]?["panel"]?["2"]);
        Assert.NotNull(jsonNode?["data"]?["panel"]?["3"]);
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

        UpdateUserExtensionsResponse? response = JsonSerializer.Deserialize<UpdateUserExtensionsResponse>(responseJson, JsonOptions);

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
