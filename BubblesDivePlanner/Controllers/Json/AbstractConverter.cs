using System;
using Newtonsoft.Json;

namespace BubblesDivePlanner.Controllers.Json
{
    public class AbstractConverter<TReal, TAbstract> : JsonConverter where TReal : TAbstract
    {
        public override Boolean CanConvert(Type objectType)
            => objectType == typeof(TAbstract);

        public override Object ReadJson(JsonReader reader, Type type, Object value, JsonSerializer jsonSerialiser)
            => jsonSerialiser.Deserialize<TReal>(reader);

        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer jsonSerialiser)
            => jsonSerialiser.Serialize(writer, value);
    }
}
