using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DataBase : BaseStorage
{
    private Data _data;

    public Data Data => _data;

    private void Awake()
    {
        _data = new Data();
    }

    private void OnEnable()
    {
        SetSaveData(_data);
        OnStorageEnable();
    }

    private void OnDisable()
    {
        OnStorageDisable();
    }

    public override void Load(SaveData saveData)
    {
        if (saveData == null || saveData.GetType() != _data.GetType())
        {
            Debug.LogError($"Save data is empty or incorrect type. {saveData.GetType()} | {nameof(Data)}");
            return;
        }

        _data = saveData as Data;
        Debug.Log("data loaded");
    }

    public override void Save()
    {

    }
}

[Serializable]
public class Data : SaveData
{
    public List<User> Users;

    public Data()
    {
        Users = new List<User>();
    }

    public void AddUser(string login, string password)
    {
        Users.Add(new User(login, password));
    }

    public User GetUser(string login, string password)
    {
        return Users.FirstOrDefault(user => user.Login == login && user.Password == password);
    }

    public bool CheckForExistUser(string login)
    {
        if (Users.FirstOrDefault(user => user.Login == login) == null)
        {
            return false;
        }
        return true;
    }
}