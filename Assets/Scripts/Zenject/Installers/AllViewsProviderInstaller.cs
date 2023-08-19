using RingInWater.View;
using UnityEngine;
using Zenject;


namespace RingInWater.Common.Zenject
{
    public class AllViewsProviderInstaller : MonoInstaller
    {
        [SerializeField] private AllViewsProvider allViewsProviderTemplate = null;
        public override void InstallBindings()
        {
            AllViewsProvider instance = this.Container
                .InstantiatePrefabForComponent<AllViewsProvider>(this.allViewsProviderTemplate);

            this.Container.Bind<AllViewsProvider>()
                .FromInstance(instance)
                .AsSingle()
                .NonLazy();
        }
    }
}