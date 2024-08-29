using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace POCAmazonSpecflowBDDFramework.Helpers
{
    public static class JsonNodeExtensions
    {
        public static T GetValue<T>(this JsonNode node)
        {
            if (node is JsonValue jsonValue)
            {
                return jsonValue.TryGetValue(out T result) ? result : default;
            }
            return default;
        }
    }
}
