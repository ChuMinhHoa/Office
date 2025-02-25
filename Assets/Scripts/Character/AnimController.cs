using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Character
{
    public class AnimController : MonoBehaviour
    {
        private static readonly int AnimIndex = Animator.StringToHash("AnimIndex");
        [SerializeField] private Animator animator;

        private Sequence _mySequence;
        private UnityAction _actionCallBack;
        
        [Button]
        public void PlayAnim(AnimLayer layer, int animIndex)
        {
            SetAnimWeight(layer);
            animator.SetInteger(AnimIndex, animIndex);
        }
        
        void SetAnimWeight(AnimLayer layer)
        {
            _mySequence?.Kill();
            _mySequence = DOTween.Sequence();
            // Set all layer weights to 0
            for (int i = 0; i < animator.layerCount; i++)
            {
                if ((int)layer == i)
                    continue;
                int layerTemp = i;
                DOVirtual.Float(animator.GetLayerWeight((int)layerTemp), 0f, 0.25f,
                    value => animator.SetLayerWeight(layerTemp, value));
            }

            // Set the specified layer weight to 1
            _mySequence.Append(
                DOVirtual.Float(animator.GetLayerWeight((int)layer), 1f, 0.25f, value =>
                {
                    animator.SetLayerWeight((int)layer, value);
                })
            );
            _mySequence.Play();
            _mySequence.OnComplete(() =>
            {
                _actionCallBack?.Invoke();
                _actionCallBack = null;
                animator.SetInteger(AnimIndex, -1);
            });
           
        }
        
        [Button]
        public void PlayAnim(AnimLayer layer, bool isPlay, string animName)
        {
            if (isPlay)
                SetAnimWeight(layer);
            animator.SetBool(animName, isPlay);
        }
        
        [Button]
        public void PlayAnim(AnimLayer layer, int animIndex, UnityAction callBack)
        {
            _actionCallBack = callBack;
            SetAnimWeight(layer);
            animator.SetInteger(AnimIndex, animIndex);
        }
    }

    public enum AnimLayer
    {
        None = -1,
        Idle = 0,
        Move = 1,
        Sit = 2,
    }
}
