using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Predictions;
/// <summary>
/// Contains information about a newly created prediction.
/// </summary>
public record CreatePredictionResponse
{
    /// <summary>
    /// A list containing the single prediction that was created.
    /// </summary>
    public required ChatPrediction Data { get; init; }
}
