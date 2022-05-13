using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace zohobooks;

public static class ZohoSerializer
{

    public static async Task<string> SerializeAsync<T>(T value)
    {
        string json = string.Empty;
        using (var stream = new MemoryStream())
        {
            await JsonSerializer.SerializeAsync(stream, value, new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                

            });
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            json = await reader.ReadToEndAsync();
        }



        return json;
    }
    public static async Task<T> DeserializeAsync<T>(string json)
    {
        // convert string to stream
        byte[] byteArray = Encoding.ASCII.GetBytes(json);

        T obj;
        using (MemoryStream stream = new MemoryStream(byteArray))
        {
            obj = await JsonSerializer.DeserializeAsync<T>(stream, new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            });
        }

        return obj;
    }

    public static T Deserialize<T>(string json)
    {
        T obj = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        });


        return obj;
    }
}
