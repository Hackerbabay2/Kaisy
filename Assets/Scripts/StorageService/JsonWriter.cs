using System;
using System.IO;
using System.Xml;
using UnityEngine;
using Newtonsoft.Json;

public class JsonWriter : IStorageService
{
    public void Load<T>(string key, Action<T> callback)
    {
        string path = BuildPath(key);

        if (!File.Exists(path))
        {
            callback.Invoke(default);
            return;
        }

        using (var fileStream = new StreamReader(path))
        {
            var json = fileStream.ReadToEnd();
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                NullValueHandling = NullValueHandling.Ignore
            };

            var data = JsonConvert.DeserializeObject<T>(json, settings);
            callback.Invoke(data);
        }

    }

    public void Save(string key, object data, Action<bool> callback = null)
    {
        string path = BuildPath(key);
        var directory = Path.GetDirectoryName(path);

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Newtonsoft.Json.Formatting.Indented,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            NullValueHandling = NullValueHandling.Ignore
        };

        try
        {
            string json = JsonConvert.SerializeObject(data, settings);

            using (var fileStream = new StreamWriter(path))
            {
                fileStream.Write(json);
            }

            callback?.Invoke(true);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Save failed: {ex.Message}");
            callback?.Invoke(false);
        }
    }

    private string BuildPath(string key)
    {
        return Path.Combine(Application.persistentDataPath, key);
    }
}