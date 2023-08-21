using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PortaCapena.OdooJsonRpcClient.Attributes;
using PortaCapena.OdooJsonRpcClient.Converters;
using PortaCapena.OdooJsonRpcClient.Models;
using System.Runtime.Serialization;

namespace APIOdooWeb.Models
{
    [OdooTableName("product.product")]
    [JsonConverter(typeof(OdooModelConverter))]
    public class OdooProductProduct: IOdooModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("price")]
        public double? Price { get; set; }

        // product.template
        [JsonProperty("product_tmpl_id")]
        public int ProductTmplId { get; set; }

        [JsonProperty("activity_exception_decoration")]
        public ActivityExceptionDecorationOdooEnum? ActivityExceptionDecoration { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ActivityExceptionDecorationOdooEnum
    {
        [EnumMember(Value = "warning")]
        Alert = 1,

        [EnumMember(Value = "danger")]
        Error = 2,
    }
}
