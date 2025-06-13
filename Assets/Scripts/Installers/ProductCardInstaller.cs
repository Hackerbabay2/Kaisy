using UnityEngine;
using Zenject;

public class ProductCardInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ProductCardInitializer>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}