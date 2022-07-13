using _1._ESCLite_Task.Scripts.System;
using _1._ESCLite_Task.Scripts.Unity.Configs;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace _1._ESCLite_Task.Scripts.Unity
{
    public class GameInstaller : MonoInstaller
    {
        [field: SerializeField]
        private PlayerConfig PlayerConfig { get; set; }
        
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<EscBootstrap>()
                .FromNew()
                .AsSingle();
            
            Container
                .Bind<EcsWorld>()
                .FromNew()
                .AsSingle();

            Container
                .Bind<PlayerSystem>()
                .FromNew()
                .AsSingle();
            
            Container
                .Bind<InputSystem>()
                .FromNew()
                .AsSingle();
            
            Container
                .Bind<KeyDoorSystem>()
                .FromNew()
                .AsSingle();
            
            BindSharedData();

            void BindSharedData()
            {
                var sharedData = new EscBootstrap.SharedData
                {
                    PlayerMovementSpeed = PlayerConfig.MovementSpeed,
                    GroundLayerMask = PlayerConfig.GroundLayerMask
                };
                
                Container
                    .Bind<EscBootstrap.SharedData>()
                    .FromInstance(sharedData)
                    .AsSingle();
            }
        }
    }
}
