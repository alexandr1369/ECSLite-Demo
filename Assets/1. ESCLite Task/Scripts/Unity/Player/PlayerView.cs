using _1._ESCLite_Task.Scripts.Component;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace _1._ESCLite_Task.Scripts.Unity.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerView : MonoBehaviour
    {
        private const float MIN_PLAYER_MOVEMENT_DISTANCE = .1f;
        
        
        #region Fields

        private EcsWorld _ecsWorld;
        
        private PlayerAnimator _playerAnimator;
        
        private string _id;

        #endregion


        [Inject]
        private void Construct(EcsWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
            _id = GetHashCode().ToString();
            var playerEntity = _ecsWorld.NewEntity();
            var inputEntity = _ecsWorld.NewEntity();
            var inputComponentsPool = _ecsWorld.GetPool<InputComponent>();
            var playerComponentsPool = _ecsWorld.GetPool<PlayerComponent>();
            var transformComponentsPool = _ecsWorld.GetPool<TransformComponent>();
            
            ref var inputComponent = ref inputComponentsPool.Add(inputEntity);
            ref var playerComponent = ref playerComponentsPool.Add(playerEntity);
            transformComponentsPool.Add(playerEntity);
            inputComponent.Id = _id;
            playerComponent.Id = _id;
        }

        private void Awake()
        {
            var animator = GetComponent<Animator>();
            _playerAnimator = new PlayerAnimator();
            _playerAnimator.Init(animator);
        }

        private void Update()
        {
            var inputs = _ecsWorld.Filter<InputComponent>().End();
            var playerEntities = _ecsWorld.Filter<PlayerComponent>().Inc<TransformComponent>().End();
            
            var inputComponentsPool = _ecsWorld.GetPool<InputComponent>();
            var playerComponentsPool = _ecsWorld.GetPool<PlayerComponent>();
            var transformComponentsPool = _ecsWorld.GetPool<TransformComponent>();

            foreach (var playerEntity in playerEntities)
            {
                var playerComponent = playerComponentsPool.Get(playerEntity);
                ref var transformComponent = ref transformComponentsPool.Get(playerEntity);
                
                if(!playerComponent.Id.Equals(_id))
                    continue;

                if (!transformComponent.Enabled)
                {
                    transformComponent.Enabled = true;
                    transformComponent.Position = transform.position;
                    _playerAnimator.SetState(false);
                }
                else
                {
                    foreach (var input in inputs)
                    {
                        var inputComponent = inputComponentsPool.Get(input);
                        var lookAtPosition = new Vector3(inputComponent.Position.x,
                            transformComponent.Position.y, inputComponent.Position.z);
                        var playerToInputDistance = (lookAtPosition - transformComponent.Position).magnitude;
                        
                        if(!inputComponent.Id.Equals(playerComponent.Id)
                           || !inputComponent.Enabled
                           || playerToInputDistance <= MIN_PLAYER_MOVEMENT_DISTANCE)
                        {
                            _playerAnimator.SetState(false);
                            continue;
                        }
                        
                        transform.position = transformComponent.Position;
                        _playerAnimator.SetState(true);
                    
                        transform.LookAt(lookAtPosition);
                    }
                }
            }
        }
    }
}

