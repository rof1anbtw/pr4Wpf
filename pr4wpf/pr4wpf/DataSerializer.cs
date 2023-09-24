using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

public static class DataSerializer
{
    public static void SerializeData<T>(string filePath, List<T> data)
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public static List<T> DeserializeData<T>(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
        else
        {
            return new List<T>();
        }
    }
}
