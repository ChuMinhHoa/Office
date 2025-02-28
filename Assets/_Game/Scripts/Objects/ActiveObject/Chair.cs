namespace _Game.Scripts.Objects.ActiveObject
{
    public class Chair : NoneStaticObj
    {
        private OpenInteractObj openInteractObj;

        public override void InitInteract()
        {
            openInteractObj = new OpenInteractObj();
            InteractActions.Add(InteractType.Open, openInteractObj.OnInteract);
        }
    }
}
