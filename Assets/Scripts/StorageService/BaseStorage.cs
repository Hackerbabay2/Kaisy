using UnityEngine;
using Zenject;

public abstract class BaseStorage : MonoBehaviour
{
    [Inject] StorageService _storageService;

    protected SaveData _saveData;

    public SaveData SaveData => _saveData;

    public void SetSaveData(SaveData saveData)
    {
        _saveData = saveData;
    }

    protected void OnStorageEnable()
    {
        _storageService?.RegisterSaveable(this);
    }

    protected void OnStorageDisable()
    {
        _storageService?.UnregisterSaveable(this);
    }

    public virtual void InitData(SaveData saveData)
    {
        Save();
        _storageService.GameData.AddData(GetSaveKey(), saveData);
    }

    public abstract void Save();

    public abstract void Load(SaveData saveData);

    public string GetSaveKey()
    {
        return $"{gameObject.scene.name}_{gameObject.name}";
    }
}