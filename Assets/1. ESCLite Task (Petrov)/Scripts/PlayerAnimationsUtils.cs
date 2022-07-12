using UnityEngine;

namespace _1._ESCLite_Task__Petrov_.Scripts
{
    public class PlayerAnimationsUtils : MonoBehaviour
    {
        [field: SerializeField]
        private AudioClip[] FootstepAudioClips { get; set; }
        
        [field: Range(0, 1)]
        [field: SerializeField]
        private float FootstepAudioVolume { get; set; } = 0.5f;
        

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.position, FootstepAudioVolume);
                }
            }
        }
    }
}