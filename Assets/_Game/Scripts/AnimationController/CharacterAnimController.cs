namespace _Game.Scripts.AnimationController
{
    public class CharacterAnimController : BaseAnimController<AnimPlayerLayer>
    {
        public override int GetLayerIndex()
        {
            return (int)currentAnimLayer;
        }
    }

    public enum AnimPlayerLayer
    {
        None = -1,
        Idle = 0,
        Move = 1,
        Sit = 2,
    }
}
