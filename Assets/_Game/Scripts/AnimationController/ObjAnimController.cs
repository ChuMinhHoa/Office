namespace _Game.Scripts.AnimationController
{
    public class ObjAnimController : BaseAnimController<AnimObjLayer>
    {
        public override int GetLayerIndex()
        {
            return (int)currentAnimLayer;
        }
    }
    
    public enum AnimObjLayer
    {
        None = -1,
        Idle = 0,
        Work = 1,
        Open = 2,
        Close = 3,
    }
}
