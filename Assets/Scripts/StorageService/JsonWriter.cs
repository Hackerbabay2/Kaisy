using System;
using System.IO;
using UnityEngine;

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

            var data = JsonUtility.FromJson<T>(path);
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

        try
        {
            string json = JsonUtility.ToJson(data);

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