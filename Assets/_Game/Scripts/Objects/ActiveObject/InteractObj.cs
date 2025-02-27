using UnityEngine;

namespace Objects.ActiveObject
{
    public interface IInteractObj
    {
        public void OnInteract();
    }
    
    public class OpenInteractObj : IInteractObj
    {
        public void OnInteract()
        {
            Debug.Log("On Interact Open");
        }
    }
}
