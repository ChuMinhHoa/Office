namespace _Game.Scripts.Objects.ActiveObject
{
    public class Chair : NoneStaticObj
    {
        private OpenInteractObj _openInteractObj;

        public override void InitInteract()
        {
            _openInteractObj = new OpenInteractObj();
            InteractActions.Add(InteractType.Open, _openInteractObj.OnInteract);
        }
    }
}
