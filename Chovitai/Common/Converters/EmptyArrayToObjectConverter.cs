using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chovitai.Common.Converters
{
    public class EmptyArrayToObjectConverter<T> : JsonConverter<T>
    {
        public override T Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            var rootElement = JsonDocument.ParseValue(ref reader);

            // if its array return new instance or null
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                // return default(T); // if you want null value instead of new instance
                return default(T)!;
            }
            else
            {
                var text = rootElement.RootElement.GetRawText();
                return JsonSerializer.Deserialize<T>(text, options)!;
            }
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return true;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize<T>(writer, value, options);
        }
    }
}
