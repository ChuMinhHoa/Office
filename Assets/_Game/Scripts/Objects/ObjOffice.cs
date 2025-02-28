using System;
using System.Collections.Generic;
using _Game.Scripts.ScriptableObject;
using UnityEngine;
using UnityEngine.Serialization;

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

    public enum ObjType
    {
        None,
        Table,
        Chair,
        PC,
    }

    public class ObjOffice : MonoBehaviour, IObj
    {
        public ObjStaticType staticType;
        public Dictionary<InteractType, Action> InteractActions = new();
        public ObjConfig objConfig;
        public ObjType objType;
        public void Awake()
        {
            InitObj();
            InitInteract();
        }

        public virtual void InitObj()
        {
            objConfig = ObjDataConfig.Instance.GetConfig(objType);
        }

        public void OnInteract(InteractType interactType)
        {
            if (InteractActions.TryGetValue(interactType, out var action))
                action.Invoke();
            else
               Debug.Log($"None action for this interactType {interactType}");
        }

        public void OnMoveObj() { }
        public virtual void InitInteract() { }
        public ObjConfig GetObjData() => objConfig;
    }
}
