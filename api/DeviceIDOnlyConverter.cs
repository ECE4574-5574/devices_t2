using System;
using Newtonsoft.Json;
using api;

namespace api.Converters
{
public class DeviceIDOnlyConverter : JsonConverter
{
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(FullID);
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		var id = (FullID)value;
		writer.WriteValue(id.DeviceID);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		var id = new FullID();
		id.DeviceID = (UInt64)reader.Value;
		return id;
	}
}
}

