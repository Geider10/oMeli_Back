using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace oMeli_Back.Utils
{
    public class DateConverter : JsonConverter<DateTime>
    {
        public string format = "dd/MM/yyyy";
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), format, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(format));
        }
    }
}
