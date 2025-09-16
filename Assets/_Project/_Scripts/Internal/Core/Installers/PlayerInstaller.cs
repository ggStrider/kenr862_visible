using Core.DataModel;
using Internal.Core.Inputs;
using Internal.Gameplay.Environment.Rooms;
using KinematicCharacterController.Examples;
using UnityEngine;
using Zenject;

namespace Internal.Core.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private ExamplePlayer _examplePlayer;

        [Space] [SerializeField] private PlayerRoomManager _playerRoomManager;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputReader>()
                .AsSingle()
                .NonLazy();

            Container.Bind<Inventory>()
                .FromInstance(_inventory)
                .AsSingle()
                .NonLazy();

            Container.Bind<ExamplePlayer>()
                .FromInstance(_examplePlayer)
                .AsSingle()
                .NonLazy();

            Container.Bind<PlayerRoomManager>()
                .FromInstance(_playerRoomManager)
                .AsSingle()
                .NonLazy();
        }
    }
}