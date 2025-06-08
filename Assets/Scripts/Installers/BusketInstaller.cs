using UnityEngine;
using Zenject;

public class BusketInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<BusketLoader>().FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
    }
}