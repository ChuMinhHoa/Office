namespace _Game.Scripts.Objects.ActiveObject
{
    public class Chair : NoneStaticObj
    {
        private ShitDownInteractObj _shitDownInteractObj;

        public override void InitInteract()
        {
            _shitDownInteractObj = new ShitDownInteractObj();
            InteractActions.Add(InteractType.Open, _shitDownInteractObj.OnInteract);
        }
    }
}
