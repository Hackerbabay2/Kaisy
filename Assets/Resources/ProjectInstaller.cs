using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private StorageService _storageServicePrefab;
    [SerializeField] private DataBase _dataBasePrefab;

    public override void InstallBindings()
    {
        Container.Bind<UserData>().FromNew().AsSingle().NonLazy();
        Container.Bind<IStorageService>().To<JsonWriter>().AsSingle().NonLazy();
        Container.Bind<StorageService>().FromComponentInNewPrefab(_storageServicePrefab)
            .AsSingle().NonLazy();
        Container.Bind<DataBase>().FromComponentInNewPrefab(_dataBasePrefab).AsSingle().NonLazy();
    }
}