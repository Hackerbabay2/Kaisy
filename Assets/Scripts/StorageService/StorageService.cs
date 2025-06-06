using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StorageService : MonoBehaviour
{
    [Inject] private IStorageService _storageService;

    private GameData _gameData;
    private List<BaseStorage> _saveables;

    public GameData GameData => _gameData;

    private void Awake()
    {
        _saveables = new List<BaseStorage>();
        _gameData = new GameData(new Dictionary<string, SaveData>());
    }

    public void RegisterSaveable(BaseStorage saveable)
    {
        if (!_saveables.Contains(saveable))
        {
            _saveables.Add(saveable);
        }
    }

    public void UnregisterSaveable(BaseStorage saveable)
    {
        _saveables.Remove(saveable);
    }

    private string GetKey()
    {
        return $"{gameObject.scene.name}_{gameObject.name}";
    }

    public IEnumerator Save()
    {
        foreach (var saveable in _saveables)
        {
            saveable.InitData(saveable.SaveData);
            yield return null;
        }

        _storageService.Save(GetKey(), _gameData);
        Debug.Log("Save complete");
    }

    public IEnumerator Load()
    {
        foreach (var saveable in _saveables)
        {
            var saveKey = saveable.GetSaveKey();

            if (_gameData.SaveableObjects.TryGetValue(saveKey, out var savedData))
            {
                saveable.Load(savedData);
            }
            yield return null;
        }
    }
}