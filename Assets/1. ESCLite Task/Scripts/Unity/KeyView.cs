using _1._ESCLite_Task.Scripts.Component;
using _1._ESCLite_Task.Scripts.Unity.Player;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace _1._ESCLite_Task.Scripts.Unity
{
    public class KeyView : MonoBehaviour
    {
        [field: SerializeField]
        private DoorView DoorView { get; set; }


        #region Fields

        private EcsWorld _ecsWorld;
        
        private string _id;

        #endregion

        
        [Inject]
        private void Construct(EcsWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
            _id = GetHashCode().ToString();
            var keyEntity = _ecsWorld.NewEntity();
            var doorEntity = _ecsWorld.NewEntity();
            var keyComponentsPool = _ecsWorld.GetPool<KeyComponent>();
            var doorComponentsPool = _ecsWorld.GetPool<DoorComponent>();
            ref var keyComponent = ref keyComponentsPool.Add(keyEntity);
            ref var doorComponent = ref doorComponentsPool.Add(doorEntity);
            keyComponent.Id = _id;
            doorComponent.Id = _id;
            DoorView.Init(_ecsWorld, _id);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!other.GetComponent<PlayerView>()) return;
            UpdateState(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if(!other.GetComponent<PlayerView>()) return;
            UpdateState(false);
        }


        private void UpdateState(bool state)
        {
            var keys = _ecsWorld.Filter<KeyComponent>().End();
            var keyComponentsPool = _ecsWorld.GetPool<KeyComponent>();

            foreach (var key in keys)
            {
                ref var keyComponent = ref keyComponentsPool.Get(key);
                
                if(!keyComponent.Id.Equals(_id))
                    continue;
                
                keyComponent.IsPressed = state;
            }
        }
    }
}