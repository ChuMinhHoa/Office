using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    public class AnimController : MonoBehaviour
    {
        private static readonly int AnimIndex = Animator.StringToHash("AnimIndex");
        [SerializeField] private Animator animator;

        private Sequence _mySequence;
        
        [Button]
        public void PlayAnim(AnimLayer layer, int animIndex)
        {
            SetAnimWeight(layer);
            animator.SetInteger(AnimIndex, animIndex);
        }
        
        void SetAnimWeight(AnimLayer layer)
        {
            
            // Set all layer weights to 0
            for (int i = 0; i < animator.layerCount; i++)
            {
                animator.SetLayerWeight(i, 0);
            }

            // Set the specified layer weight to 1
            animator.SetLayerWeight((int)layer, 1);
        }
        
        [Button]
        public void PlayAnim(AnimLayer layer, bool isPlay, string animName)
        {
            if (isPlay)
                SetAnimWeight(layer);
            animator.SetBool(animName, isPlay);
        }
    }

    public enum AnimLayer
    {
        None=-1,
        Idle = 0,
        Move = 1,
        Sit = 2,
    }
}
