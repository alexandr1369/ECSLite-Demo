using _1._ESCLite_Task.Scripts.Component;
using Leopotam.EcsLite;
using UnityEngine;

namespace _1._ESCLite_Task.Scripts.System
{
    public class PlayerSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var ecsWorld = systems.GetWorld();
            var playerEntities = ecsWorld.Filter<PlayerComponent>().Inc<TransformComponent>().End();
            var inputs = ecsWorld.Filter<InputComponent>().End();
            
            var playerComponentsPool = ecsWorld.GetPool<PlayerComponent>();
            var transformComponentsPool = ecsWorld.GetPool<TransformComponent>();
            var inputComponentsPool = ecsWorld.GetPool<InputComponent>();
            
            var sharedData = systems.GetShared<EscBootstrap.SharedData>();
            var movementSpeed = sharedData.PlayerMovementSpeed;
            var deltaTime = sharedData.DeltaTime;
            
            foreach (var playerEntity in playerEntities)
            {
                var playerComponent = playerComponentsPool.Get(playerEntity);
                ref var transformComponent = ref transformComponentsPool.Get(playerEntity);
                
                foreach (var input in inputs)
                {
                    ref var inputComponent = ref inputComponentsPool.Get(input);
                    
                    if(!inputComponent.Id.Equals(playerComponent.Id)
                       || !inputComponent.Enabled)
                    {
                        continue;
                    }

                    var currentPosition = transformComponent.Position;
                    var inputPosition = new Vector3(inputComponent.Position.x,
                        currentPosition.y, inputComponent.Position.z);
                    var minDistanceDelta = movementSpeed * deltaTime;
                    MovePosition(ref transformComponent, inputPosition, minDistanceDelta);
                }
            }

            void MovePosition(ref TransformComponent transformComponent, 
                Vector3 targetPosition, float minDistanceDelta)
            {
                var currentPosition = transformComponent.Position;
                var movementDistanceDelta = targetPosition - currentPosition;
                var movementDistance = movementDistanceDelta.magnitude;
                var movementDirection = movementDistanceDelta.normalized;
                
                transformComponent.Position = movementDistance < minDistanceDelta
                    ? targetPosition
                    : currentPosition + movementDirection * minDistanceDelta;
            }
        }
    }
}