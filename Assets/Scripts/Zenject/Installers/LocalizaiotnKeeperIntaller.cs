using RingInWater.UI;
using UnityEngine;
using Zenject;

namespace RingInWater.Common.Zenject
{
    public class LocalizaiotnKeeperIntaller : MonoInstaller
    {
        [SerializeField] private LocalizaiotnKeeper LocalizaiotnKeeperTemplate = null;
        public override void InstallBindings()
        {
            LocalizaiotnKeeper instance = Container
                .InstantiatePrefabForComponent<LocalizaiotnKeeper>(LocalizaiotnKeeperTemplate);

            Container.Bind<LocalizaiotnKeeper>()
                .FromInstance(instance)
                .AsSingle()
                .NonLazy();
        }
    }
}