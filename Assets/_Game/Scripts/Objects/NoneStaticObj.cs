using AnimationController;
using UnityEngine.Serialization;

namespace Objects
{
    public class NoneStaticObj : ObjOffice
    {
        [FormerlySerializedAs("animController")] public CharacterAnimController characterAnimController;
        public virtual void PlayAnim(int animIndex, AnimPlayerLayer playerLayer)
        {
            characterAnimController.PlayAnim(playerLayer, animIndex);
        }
    }
}
