using UnityEngine;

namespace _Game.Scripts.Objects.ActiveObject
{
    public interface IInteractObj
    {
        public void OnInteract();
    }
    
    public class ShitDownInteractObj : IInteractObj
    {
        public void OnInteract()
        {
            Debug.Log("On Interact Open");
        }
    }
}
