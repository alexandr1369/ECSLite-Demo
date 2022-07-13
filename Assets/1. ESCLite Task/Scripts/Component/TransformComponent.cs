using UnityEngine;

namespace _1._ESCLite_Task.Scripts.Component
{
    public struct TransformComponent
    {
        [field: SerializeField]
        public bool Enabled { get; set;}
        
        [field: SerializeField]
        public Vector3 Position { get; set; }
    }
}