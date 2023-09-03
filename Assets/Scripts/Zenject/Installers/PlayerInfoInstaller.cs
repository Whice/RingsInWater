using Model;
using Zenject;

namespace RingInWater.Common.Zenject
{
    public class PlayerInfoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            PlayerInfo playerInfo = PlayerInfo.Load();

            Container.Bind<PlayerInfo>()
                .FromInstance(playerInfo)
                .AsSingle()
                .NonLazy();
        }
    }
}