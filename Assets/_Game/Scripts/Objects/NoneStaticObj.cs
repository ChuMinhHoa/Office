using System;
using _Game.Scripts.AnimationController;
using _Game.Scripts.ScriptableObject;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Objects
{
    public class NoneStaticObj : ObjOffice
    {
        [SerializeField] private ObjAnimController objAnimController;
        public AnimCharBObjConfig animData;
        public Transform trsPointCharStand;
        public override void InitObj()
        {
            base.InitObj();
            staticType = ObjStaticType.None;
        }

        public void PlayAnim(AnimCharBObjConfig animTemp)
        {
            animData = animTemp;
            objAnimController.PlayAnim(animData.objLayer, animData.animObjId);
        }
    }
}
