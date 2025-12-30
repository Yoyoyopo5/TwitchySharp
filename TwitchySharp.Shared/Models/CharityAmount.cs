using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// Contains information about charity fundraising amounts and currencies.
/// </summary>
public readonly record struct CharityAmount
{
    /// <summary>
    /// The monetary amount. 
    /// The amount is specified in the currency’s minor unit. 
    /// For example, the minor units for USD is cents, so if the amount is $5.50 USD, value is set to 550.
    /// </summary>
    public required int Value { get; init; }
    /// <summary>
    /// The number of decimal places used by the currency. 
    /// For example, USD uses two decimal places. 
    /// </summary>
    public required int DecimalPlaces { get; init; }
    /// <summary>
    /// Calculated monetary value (dollar value) as given by <see cref="Value"/> and <see cref="DecimalPlaces"/>.
    /// Note that this value is calculated each time it is called.
    /// </summary>
    public double MonetaryValue => (double)Value / Math.Pow(10, DecimalPlaces);
    /// <summary>
    /// The ISO-4217 three-letter currency code that identifies the type of currency in value.
    /// </summary>
    public required string Currency { get; init; }
}
