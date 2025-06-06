using System.Collections.Generic;

public class GameData
{
    public Dictionary<string, SaveData> SaveableObjects;

    public GameData(Dictionary<string, SaveData> saveableObjects)
    {
        SaveableObjects = saveableObjects;
    }

    public void AddData(string id, SaveData ObjectData)
    {
        if (!SaveableObjects.ContainsKey(id))
        {
            SaveableObjects.Add(id, ObjectData);
        }
    }

    public T GetData<T>(string id) where T : SaveData
    {
        if (SaveableObjects.TryGetValue(id, out SaveData data) && data is T typedData)
            return typedData;
        return null;
    }
}