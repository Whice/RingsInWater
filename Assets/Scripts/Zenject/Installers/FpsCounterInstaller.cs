
using RingInWater.Utils.FPSCounting;
using UnityEngine;
using Zenject;

namespace RingInWater.Common.Zenject
{
    public class FpsCounterInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _fpsObject;

        public override void InstallBindings()
        {
            Container.Bind<FpsDisplay>().FromComponentInNewPrefab(_fpsObject).AsSingle().NonLazy();
        }
    }
}