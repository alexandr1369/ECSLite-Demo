using UnityEngine;

namespace _1._ESCLite_Task.Scripts.Unity.Player
{
    public class PlayerAnimator
    {
        private const float NONE_SPEED = 0;
        private const float MOVEMENT_SPEED = 2f;
        
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int MotionSpeed = Animator.StringToHash("MotionSpeed");
            
            
        #region Fields

        private Animator _animator;

        #endregion


        public void Init(Animator animator)
        {
            _animator = animator;
        }
            
        public void SetState(bool state)
        {
            var value = state? MOVEMENT_SPEED : NONE_SPEED;
            _animator.SetFloat(Speed, value);
            _animator.SetFloat(MotionSpeed, value);
        }
    }
}

