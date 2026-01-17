using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchySharp.Api.ResponseConverters;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
internal class ApiConverterAttribute(Type converterType, object[]? args = null) : Attribute
{
    public IConvertResponseContent? CreateConverter()
        => Activator.CreateInstance(converterType, args) as IConvertResponseContent;
}
