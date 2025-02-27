using DG.Tweening;
using Etc;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace AnimationController
{
    public abstract class BaseAnimController<TAnimLayer> : MonoBehaviour
    {
        public TAnimLayer currentAnimLayer;
        [HideInInspector]public int currentLayerIndex;
        public int currentAnimIndex;
        [SerializeField] private Animator animator;
        private Sequence mySequence;
        private UnityAction actionCallBack;

        [Button]
        public void PlayAnim(TAnimLayer layer, int animIndex)
        {
            SetAnimWeight(layer);
            currentAnimIndex = animIndex;
            animator.SetInteger(MyCache.StrAnimIndex, animIndex);
        }

        public void SetAnimWeight(TAnimLayer layer)
        {
            currentAnimLayer = layer;
            currentLayerIndex = GetLayerIndex();
            mySequence?.Kill();
            mySequence = DOTween.Sequence();
            mySequence.Append(
                DOVirtual.Float(animator.GetLayerWeight(currentLayerIndex), 1f, 0.25f,
                    value => { animator.SetLayerWeight(currentLayerIndex, value); })
            );

            // Set all layer weights to 0
            for (int i = 0; i < animator.layerCount; i++)
            {
                if (currentLayerIndex == i)
                    continue;
                animator.SetLayerWeight(i, 0);
            }

            mySequence.Play();
            mySequence.OnComplete(() =>
            {
                actionCallBack?.Invoke();
                actionCallBack = null;
            });

        }

        public void PlayAnim(TAnimLayer layer, int animIndex, UnityAction callBack)
        {
            currentAnimLayer = layer;
            actionCallBack = callBack;
            SetAnimWeight(layer);
            currentAnimIndex = animIndex;
            animator.SetInteger(MyCache.StrAnimIndex, animIndex);
            // if (!MyCache.IsHaveAnim(layer, animIndex))
            //     MyCache.AddAnimTimeLine(layer, animIndex, AnimationTimeLineGlobalConfig.GetTimeAnimation(layer, animIndex));
        }

        public virtual int GetLayerIndex()
        {
            return 0;
        }
        
        public float GetCurrentTimeAnimation()
        {
            return MyCache.GetAnimTime(currentAnimIndex, currentAnimLayer);
        }
    }
}
