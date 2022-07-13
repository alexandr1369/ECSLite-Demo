using _1._ESCLite_Task.Scripts.Component;
using Leopotam.EcsLite;
using UnityEngine;

namespace _1._ESCLite_Task.Scripts.System
{
    public class KeyDoorSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var ecsWorld = systems.GetWorld();
            var keyEntities = ecsWorld.Filter<KeyComponent>().End();
            var doorEntities = ecsWorld.Filter<DoorComponent>().End();
            var keyComponentsPool = ecsWorld.GetPool<KeyComponent>();
            var doorComponentsPool = ecsWorld.GetPool<DoorComponent>();
            
            foreach (var keyEntity in keyEntities)
            {
                ref var keyComponent = ref keyComponentsPool.Get(keyEntity);

                foreach (var doorEntity in doorEntities)
                {
                    ref var doorComponent = ref doorComponentsPool.Get(doorEntity);
                    
                    if(!doorComponent.Id.Equals(keyComponent.Id))
                        continue;

                    doorComponent.IsOpening = keyComponent.IsPressed;
                }
            }
        }
    }
}

