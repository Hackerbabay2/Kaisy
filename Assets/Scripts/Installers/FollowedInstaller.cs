using UnityEngine;
using Zenject;

public class FollowedInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<FollowedLoader>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}