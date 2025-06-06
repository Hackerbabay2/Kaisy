using UnityEngine;
using Zenject;

public class ApplicationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IStorageService>().To<JsonWriter>().AsSingle();
        Container.Bind<StorageService>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DataBase>().FromComponentInHierarchy().AsSingle();
    }
}