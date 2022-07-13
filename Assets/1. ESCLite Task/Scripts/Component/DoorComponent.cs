using UnityEngine;

namespace _1._ESCLite_Task.Scripts.Component
{
    public struct DoorComponent
    {
        [field: SerializeField]
        public string Id { get; set; }
        
        [field: SerializeField]
        public bool IsOpening { get; set; }
    }
}
