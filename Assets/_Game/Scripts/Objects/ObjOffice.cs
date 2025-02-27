using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Objects
{
    public interface IObj
    {
        public void OnInteract(InteractType interactType);
        public void OnMoveObj();
        public void InitInteract();
    }

    public enum ObjStaticType
    {
        None,
        Static,
    }
    
    public enum InteractType
    {
        None,
        Work,
        Open,
        Close
    }
    
    public class ObjOffice : MonoBehaviour, IObj
    {
        public ObjStaticType objStaticType;
        public Dictionary<InteractType, Action> InteractActions = new();

        public void OnInteract(InteractType interactType)
        {
            if (InteractActions.TryGetValue(interactType, out var action))
                action.Invoke();
            else
               Debug.Log($"None action for this interactType {interactType}");
        }

        public void OnMoveObj() { }
        public virtual void InitInteract() { }
    }
}
