using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NotificationType
    {
        PlayerChangedNickname,
        CorporationChangedName,
        PlayerChangedCorporation,
        System
    }
}
