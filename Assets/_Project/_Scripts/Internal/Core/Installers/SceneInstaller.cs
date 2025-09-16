using Cutscenes;
using Internal.Core.DataModel;
using Internal.Core.UI;
using Internal.Gameplay.GameTasks;
using UnityEngine;
using Zenject;

namespace Internal.Core.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private CutsceneManager _cutsceneManager;
        [SerializeField] private HardCodedShitNamedGameTasks _hardCodedShit;
        
        public override void InstallBindings()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            Container.BindInterfacesAndSelfTo<EscManager>()
                .AsSingle()
                .NonLazy();

            Container.Bind<CutsceneManager>()
                .FromInstance(_cutsceneManager)
                .AsSingle()
                .NonLazy();

            Container.Bind<HardCodedShitNamedGameTasks>()
                .FromInstance(_hardCodedShit)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<PlayerData>()
                .AsSingle()
                .NonLazy();
        }
    }
}