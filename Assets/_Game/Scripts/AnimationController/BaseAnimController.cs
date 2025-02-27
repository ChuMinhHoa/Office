using _Game.Scripts.Etc;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.AnimationController
{
    public abstract class BaseAnimController<TAnimLayer> : MonoBehaviour
    {
        public TAnimLayer currentAnimLayer;
        [HideInInspector]public int currentLayerIndex;
        public int currentAnimIndex;
        [SerializeField] private Animator animator;
        private Sequence _mySequence;
        private UnityAction _actionCallBack;

        [Button]
        public void PlayAnim(TAnimLayer layer, int animIndex)
        {
            if (currentAnimIndex == animIndex && currentLayerIndex.Equals(layer))
                return;
            animator.SetInteger(MyCache.StrAnimIndex, animIndex);
            SetAnimWeight(layer);
            currentAnimIndex = animIndex;
            
        }

        public void SetAnimWeight(TAnimLayer layer)
        {
            currentAnimLayer = layer;
            currentLayerIndex = GetLayerIndex();
            _mySequence?.Kill();
            _mySequence = DOTween.Sequence();
            _mySequence.Append(
                DOVirtual.Float(animator.GetLayerWeight(currentLayerIndex), 1f, 0.5f,
                    value => { animator.SetLayerWeight(currentLayerIndex, value); })
            );

            // Set all layer weights to 0
            for (int i = 0; i < animator.layerCount; i++)
            {
                if (currentLayerIndex == i)
                    continue;
                int layerTemp = i;
                DOVirtual.Float(animator.GetLayerWeight(layerTemp), 0f, .5f,
                    value => { animator.SetLayerWeight(layerTemp, value); });
            }

            _mySequence.Play();
            _mySequence.OnComplete(() =>
            {
                _actionCallBack?.Invoke();
                _actionCallBack = null;
                animator.SetInteger(MyCache.StrAnimIndex, -1);
            });

        }

        public void PlayAnim(TAnimLayer layer, int animIndex, UnityAction callBack)
        {
            currentAnimLayer = layer;
            _actionCallBack = callBack;
            SetAnimWeight(layer);
            currentAnimIndex = animIndex;
            animator.SetInteger(MyCache.StrAnimIndex, animIndex);
        }

        public virtual int GetLayerIndex()
        {
            return 0;
        }
        
        public float GetCurrentTimeAnimation()
        {
            return AnimationTimeLineGlobalConfig.Instance.GetTimeAnimation(currentAnimLayer, currentAnimIndex);
        }
    }
}
