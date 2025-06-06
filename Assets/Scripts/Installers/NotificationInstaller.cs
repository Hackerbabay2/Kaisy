using UnityEngine;
using Zenject;

public class NotificationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<NotificationDisplayer>().FromComponentInHierarchy().AsSingle();
    }
}