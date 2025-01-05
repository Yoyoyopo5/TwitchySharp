using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Predictions;
/// <summary>
/// Contains information about the ended prediction.
/// </summary>
public record EndPredictionResponse
{
    /// <summary>
    /// A list containing the single prediction that was ended.
    /// </summary>
    public required ChatPrediction[] Data { get; init; }
}
