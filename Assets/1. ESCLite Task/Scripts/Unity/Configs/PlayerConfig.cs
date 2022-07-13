using UnityEngine;

namespace _1._ESCLite_Task.Scripts.Unity.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Zorg/Object/New Player Config", order = 1)]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField]
        public LayerMask GroundLayerMask { get; set; }
            
        [field: SerializeField]
        public float MovementSpeed { get; set; }
    }
}

