using _1._ESCLite_Task.Scripts.Component;
using _1._ESCLite_Task.Scripts.Unity.Configs;
using Leopotam.EcsLite;
using UnityEngine;

namespace _1._ESCLite_Task.Scripts.Unity
{
    [RequireComponent(typeof(Animator))]
    public class DoorView : MonoBehaviour
    {
        private const int OPENING_ANIMATION_LAYER = 0;
        
        private const float OPENING_SPEED_DIVIDER = 100f;
        
        private const string OPENING_ANIMATION_NAME = "Opening";
        
        [field: SerializeField]   
        private DoorConfig DoorConfig { get; set; }
        

        #region Fields

        private EcsWorld _ecsWorld;
        
        private Animator _animator;
        
        private string _id;
        
        private float _openingAnimationNormalizedTime;

        #endregion


        public void Init(EcsWorld ecsWorld, string id)
        {
            _ecsWorld = ecsWorld;
            _id = id;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            var doorEntities = _ecsWorld.Filter<DoorComponent>().End();
            var doorComponentsPool = _ecsWorld.GetPool<DoorComponent>();

            foreach (var doorEntity in doorEntities)
            {
                var doorComponent = doorComponentsPool.Get(doorEntity);
                
                if(!doorComponent.Id.Equals(_id))
                    continue;
                
                var openingState = doorComponent.IsOpening;
                
                if(!openingState)
                {
                    _animator.enabled = false;
                    return;
                }

                var doorOpeningSpeed = DoorConfig.OpeningSpeed;
                _openingAnimationNormalizedTime = 
                    Mathf.Clamp(_openingAnimationNormalizedTime + doorOpeningSpeed / OPENING_SPEED_DIVIDER,
                        0, 1f);
                
                SetDoorOpening();
            }

            void SetDoorOpening()
            {
                _animator.enabled = true;
                _animator.Play(OPENING_ANIMATION_NAME, OPENING_ANIMATION_LAYER, _openingAnimationNormalizedTime);
            }
        }
    }
}