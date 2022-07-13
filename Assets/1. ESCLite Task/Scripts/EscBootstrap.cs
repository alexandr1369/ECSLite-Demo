using System;
using _1._ESCLite_Task.Scripts.System;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace _1._ESCLite_Task.Scripts
{
    public class EscBootstrap : IInitializable, ITickable, IDisposable
    {
        public class SharedData
        {
            public float PlayerMovementSpeed { get; set; }
            
            public LayerMask GroundLayerMask { get; set; }
            
            public float DeltaTime { get; set; }
        }


        #region Fields

        private EcsWorld _ecsWorld;
        
        private EcsSystems _ecsSystems;
        
        private SharedData _sharedData;
        
        private PlayerSystem _playerSystem;
        
        private InputSystem _inputSystem;
        
        private KeyDoorSystem _keyDoorSystem;
        
        private LayerMask _groundLayerMask;

        #endregion


        [Inject]
        private void Construct(
            EcsWorld ecsWorld,
            SharedData sharedData,
            PlayerSystem playerSystem,
            InputSystem inputSystem,
            KeyDoorSystem keyDoorSystem)
        {
            _ecsWorld = ecsWorld;
            _sharedData = sharedData;
            _playerSystem = playerSystem;
            _inputSystem = inputSystem;
            _keyDoorSystem = keyDoorSystem;
        }
        
        public void Initialize()
        {
            _ecsSystems = new EcsSystems(_ecsWorld, _sharedData);
            _ecsSystems
                .Add(_playerSystem)
                .Add(_inputSystem)
                .Add(_keyDoorSystem)
                .Init();
        }
        
        public void Tick()
        {
            if (_sharedData != null)
            {
                _sharedData.DeltaTime = Time.deltaTime;
            }
            
            _ecsSystems?.Run();
        }
        
        public void Dispose()
        {
            if(_ecsSystems != null)
            {
                _ecsSystems.Destroy();
                _ecsSystems = null;
            }

            if (_ecsWorld != null)
            {
                _ecsWorld.Destroy();
                _ecsWorld = null;
            }
        }
    }
}