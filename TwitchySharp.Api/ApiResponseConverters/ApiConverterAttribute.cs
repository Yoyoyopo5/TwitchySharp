using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchySharp.Api.ApiResponseConverters;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
internal class ApiConverterAttribute(Type converterType, object[]? args = null) : Attribute
{
    public IConvertApiResponse? CreateConverter()
        => Activator.CreateInstance(converterType, args) as IConvertApiResponse;
}
