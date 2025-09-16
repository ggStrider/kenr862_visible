using Zenject;

namespace Internal.Core.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>()
                .AsSingle()
                .NonLazy();

            // Container.Bind<PlayerData>()
            //     .AsSingle()
            //     .NonLazy();
        }
    }
}
