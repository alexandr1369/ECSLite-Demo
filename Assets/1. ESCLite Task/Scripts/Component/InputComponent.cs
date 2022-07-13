using UnityEngine;

namespace _1._ESCLite_Task.Scripts.Component
{
    public struct InputComponent
    {
        [field: SerializeField]
        public string Id { get; set; }
        
        [field: SerializeField]
        public bool Enabled { get; set; }
        
        [field: SerializeField]
        public Vector3 Position { get; set; }
    }
}