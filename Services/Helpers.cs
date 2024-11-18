using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace DapperWebApiExample.Services
{
    public class Helpers
    {
        public static string DefaultParamsPost(string sql, dynamic payload = null)
        {
            string newsql = sql;

            if (payload.ValueKind == JsonValueKind.Object)
            {
                foreach (var property in payload.EnumerateObject())
                {
                    string key = property.Name;
                    JsonElement value = property.Value;
                    string chkValue = "";

                    switch (value.ValueKind)
                    {
                        case JsonValueKind.String:
                            chkValue = value.GetString();
                            break;
                        case JsonValueKind.Number:
                            chkValue = value.GetInt32().ToString();
                            break;
                        case JsonValueKind.Object:
                            Console.WriteLine($"Object: {key} = {value}");
                            break;
                        case JsonValueKind.Array:
                            Console.WriteLine($"Array: {key} = {value}");
                            break;
                        default:
                            Console.WriteLine($"Unknown Type: {key} = {value}");
                            break;
                    }

                    newsql = newsql.Replace("[@" + key + "]", (!string.IsNullOrWhiteSpace(chkValue) ? "'" + chkValue + "'" : "null"));
                }

            }

            return newsql;
        }

        public static string DefaultParamsGet(string sql, string key = null, string payload = null)
        {
            string newsql = sql;

            if (!string.IsNullOrWhiteSpace(key) && payload != null)
            {
                newsql = newsql.Replace("[@" + key + "]", !string.IsNullOrWhiteSpace(payload) ? "'" + payload + "'" : "null");
            }

            return newsql;
        }
    }
}
