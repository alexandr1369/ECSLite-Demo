using UnityEngine;

namespace _1._ESCLite_Task.Scripts.Unity.Configs
{
    [CreateAssetMenu(fileName = "DoorConfig", menuName = "Zorg/Object/New Door Config", order = 2)]
    public class DoorConfig : ScriptableObject
    {
        [field: SerializeField]
        public float OpeningSpeed { get; set; }
    }
}