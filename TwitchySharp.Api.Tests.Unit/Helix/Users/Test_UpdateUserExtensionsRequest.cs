using System.Text.Json;
using System.Text.Json.Nodes;
using TwitchySharp.Api.Helix.Users;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Helix.Users;

public class Test_UpdateUserExtensionsRequest
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    [Fact]
    public void Serialize_PanelExtensions_ProducesCorrectJsonStructure()
    {
        var config = new ExtensionsConfiguration
        {
            PanelExtensions = new ExtensionsConfigurationType<UpdateExtensionParameters>()
                .ConfigureExtension(
                    new ExtensionId("rh6jq1q334hqc2rr1qlzqbvwlfl3x0"),
                    new ExtensionVersion("1.1.0"),
                    new UpdateExtensionParameters { Active = true })
        };
        var requestData = new UpdateUserExtensionsRequestData(config);

        var json = JsonSerializer.Serialize(requestData, JsonOptions);
        var jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        var panelNode = jsonNode["data"]?["panel"]?["1"];
        Assert.NotNull(panelNode);
        Assert.True(panelNode["active"]?.GetValue<bool>());
        Assert.Equal("rh6jq1q334hqc2rr1qlzqbvwlfl3x0", panelNode["id"]?.GetValue<string>());
        Assert.Equal("1.1.0", panelNode["version"]?.GetValue<string>());
    }

    [Fact]
    public void Serialize_MultiplePanelExtensions_UsesCorrect1BasedKeys()
    {
        var config = new ExtensionsConfiguration
        {
            PanelExtensions = new ExtensionsConfigurationType<UpdateExtensionParameters>()
                .ConfigureExtension(
                    new ExtensionId("ext1"),
                    new ExtensionVersion("1.0.0"),
                    new UpdateExtensionParameters { Active = true })
                .ConfigureExtension(
                    new ExtensionId("ext2"),
                    new ExtensionVersion("2.0.0"),
                    new UpdateExtensionParameters { Active = false })
        };
        var requestData = new UpdateUserExtensionsRequestData(config);

        var json = JsonSerializer.Serialize(requestData, JsonOptions);
        var jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        var panelNode = jsonNode["data"]?["panel"];
        Assert.NotNull(panelNode);
        Assert.NotNull(panelNode["1"]);
        Assert.NotNull(panelNode["2"]);
    }

    [Fact]
    public void Serialize_OverlayExtensions_ProducesCorrectJsonStructure()
    {
        var config = new ExtensionsConfiguration
        {
            OverlayExtensions = new ExtensionsConfigurationType<UpdateExtensionParameters>()
                .ConfigureExtension(
                    new ExtensionId("zfh2irvx2jb4s60f02jq0ajm8vwgka"),
                    new ExtensionVersion("1.0.19"),
                    new UpdateExtensionParameters { Active = true })
        };
        var requestData = new UpdateUserExtensionsRequestData(config);

        var json = JsonSerializer.Serialize(requestData, JsonOptions);
        var jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        var overlayNode = jsonNode["data"]?["overlay"]?["1"];
        Assert.NotNull(overlayNode);
        Assert.True(overlayNode["active"]?.GetValue<bool>());
        Assert.Equal("zfh2irvx2jb4s60f02jq0ajm8vwgka", overlayNode["id"]?.GetValue<string>());
        Assert.Equal("1.0.19", overlayNode["version"]?.GetValue<string>());
    }

    [Fact]
    public void Serialize_ComponentExtensions_IncludesXYCoordinates()
    {
        var config = new ExtensionsConfiguration
        {
            ComponentExtensions = new ExtensionsConfigurationType<UpdateComponentExtensionParameters>()
                .ConfigureExtension(
                    new ExtensionId("lqnf3zxk0rv0g7gq92mtmnirjz2cjj"),
                    new ExtensionVersion("0.0.1"),
                    new UpdateComponentExtensionParameters { Active = true, X = 0, Y = 0 })
        };
        var requestData = new UpdateUserExtensionsRequestData(config);

        var json = JsonSerializer.Serialize(requestData, JsonOptions);
        var jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        var componentNode = jsonNode["data"]?["component"]?["1"];
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
        var config = new ExtensionsConfiguration
        {
            PanelExtensions = new ExtensionsConfigurationType<UpdateExtensionParameters>()
                .ConfigureExtension(
                    new ExtensionId("panel_ext"),
                    new ExtensionVersion("1.0.0"),
                    new UpdateExtensionParameters { Active = true }),
            OverlayExtensions = new ExtensionsConfigurationType<UpdateExtensionParameters>()
                .ConfigureExtension(
                    new ExtensionId("overlay_ext"),
                    new ExtensionVersion("2.0.0"),
                    new UpdateExtensionParameters { Active = true }),
            ComponentExtensions = new ExtensionsConfigurationType<UpdateComponentExtensionParameters>()
                .ConfigureExtension(
                    new ExtensionId("component_ext"),
                    new ExtensionVersion("3.0.0"),
                    new UpdateComponentExtensionParameters { Active = true, X = 100, Y = 200 })
        };
        var requestData = new UpdateUserExtensionsRequestData(config);

        var json = JsonSerializer.Serialize(requestData, JsonOptions);
        var jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        Assert.NotNull(jsonNode["data"]?["panel"]?["1"]);
        Assert.NotNull(jsonNode["data"]?["overlay"]?["1"]);
        Assert.NotNull(jsonNode["data"]?["component"]?["1"]);
    }

    [Fact]
    public void Serialize_EmptyConfiguration_ProducesNullExtensionTypes()
    {
        var config = new ExtensionsConfiguration();
        var requestData = new UpdateUserExtensionsRequestData(config);

        var json = JsonSerializer.Serialize(requestData, JsonOptions);
        var jsonNode = JsonNode.Parse(json);

        Assert.NotNull(jsonNode);
        Assert.Null(jsonNode["data"]?["panel"]);
        Assert.Null(jsonNode["data"]?["overlay"]);
        Assert.Null(jsonNode["data"]?["component"]);
    }

    [Fact]
    public void ExtensionsConfigurationType_ChainedConfigureExtension_AccumulatesExtensions()
    {
        var config = new ExtensionsConfigurationType<UpdateExtensionParameters>()
            .ConfigureExtension(
                new ExtensionId("ext1"),
                new ExtensionVersion("1.0.0"),
                new UpdateExtensionParameters { Active = true })
            .ConfigureExtension(
                new ExtensionId("ext2"),
                new ExtensionVersion("2.0.0"),
                new UpdateExtensionParameters { Active = false })
            .ConfigureExtension(
                new ExtensionId("ext3"),
                new ExtensionVersion("3.0.0"),
                new UpdateExtensionParameters { Active = true });

        var fullConfig = new ExtensionsConfiguration { PanelExtensions = config };
        var requestData = new UpdateUserExtensionsRequestData(fullConfig);
        var json = JsonSerializer.Serialize(requestData, JsonOptions);
        var jsonNode = JsonNode.Parse(json);

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

        var response = JsonSerializer.Deserialize<UpdateUserExtensionsResponse>(responseJson, JsonOptions);

        Assert.NotNull(response);
        Assert.NotNull(response.Data);

        var panel1 = response.Data.Panel["1"];
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
