using Internal.Gameplay.Puzzle;
using Internal.Gameplay.UI;
using UnityEngine;
using Zenject;

namespace Internal.Core.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private UFourDigitCodeEnterer _uiForCodeEntererEnter;
        [SerializeField] private UCassettes _uCassettes;
        [SerializeField] private UTutorialTextThrower _tutorialTextThrower;
        [SerializeField] private UFader _fader;
        
        public override void InstallBindings()
        {
            Container.Bind<UFourDigitCodeEnterer>()
                .FromInstance(_uiForCodeEntererEnter)
                .AsSingle()
                .NonLazy();

            Container.Bind<UCassettes>()
                .FromInstance(_uCassettes)
                .AsSingle()
                .NonLazy();

            Container.Bind<UTutorialTextThrower>()
                .FromInstance(_tutorialTextThrower)
                .AsSingle()
                .NonLazy();

            Container.Bind<UFader>()
                .FromInstance(_fader)
                .AsSingle()
                .NonLazy();
        }
    }
}