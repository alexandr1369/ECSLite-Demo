using _1._ESCLite_Task.Scripts.Component;
using Leopotam.EcsLite;
using UnityEngine;

namespace _1._ESCLite_Task.Scripts.System
{
    public class InputSystem : IEcsInitSystem, IEcsRunSystem
    {
        #region Fields

        private EcsWorld _ecsWorld;
        
        private Camera _camera;
        
        private bool _hasInput;

        #endregion


        public void Init(EcsSystems systems)
        {
            _ecsWorld = systems.GetWorld();
            _camera = Camera.main;
        }
        
        public void Run(EcsSystems systems)
        {
            _hasInput = Input.GetMouseButton(0);
            
            var inputs = _ecsWorld.Filter<InputComponent>().End();
            var playerEntities = _ecsWorld.Filter<PlayerComponent>().Inc<TransformComponent>().End();

            var inputComponentsPool = _ecsWorld.GetPool<InputComponent>();
            var playerComponentsPool = _ecsWorld.GetPool<PlayerComponent>();
            
            var groundLayerMask = systems.GetShared<EscBootstrap.SharedData>().GroundLayerMask;

            if(!_hasInput) 
                return;
                
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, groundLayerMask))
                return;
            
            var hitPoint = hit.point;

            foreach(var playerEntity in playerEntities)
            {
                var playerComponent = playerComponentsPool.Get(playerEntity);
                
                foreach (var input in inputs)
                {
                    ref var inputComponent = ref inputComponentsPool.Get(input);
                    
                    if(!playerComponent.Id.Equals(inputComponent.Id))
                        continue;

                    inputComponent.Enabled = true;
                    inputComponent.Position = hitPoint;
                }
            }
        }
    }
}

