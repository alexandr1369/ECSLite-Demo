using _1._ESCLite_Task.Scripts.Component;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace _1._ESCLite_Task.Scripts.Unity.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerView : MonoBehaviour
    {
        #region Fields

        private EcsWorld _ecsWorld;
        
        private PlayerAnimator _playerAnimator;
        
        private Vector3 _lastPosition;
        
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
            // var inputComponentsPool = _ecsWorld.GetPool<InputComponent>();
            var playerEntities = _ecsWorld.Filter<PlayerComponent>().Inc<TransformComponent>().End();
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
                    _lastPosition = transformComponent.Position;
                }
                else
                {
                    // TODO: смотреть в сторону движения и регулировать скорость
                    
                    var direction = (transformComponent.Position - _lastPosition).normalized;
                    // Debug.Log(direction);
                    transform.position = transformComponent.Position;
                    _playerAnimator.SetState(true);
                    // transform.LookAt(direction);
                    // _lastPosition = transform.position;
                }
            }
        }
    }
}

