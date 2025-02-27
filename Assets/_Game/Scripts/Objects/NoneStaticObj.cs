using _Game.Scripts.AnimationController;
using UnityEngine.Serialization;

namespace _Game.Scripts.Objects
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
