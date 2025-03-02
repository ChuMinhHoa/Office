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

        private void PlayAnim()
        {
            objAnimController.PlayAnim(animData.objLayer, animData.animObjId);
        }

        public void OutWork()
        {
            objAnimController.PlayAnim(AnimObjLayer.Idle, 2);
        }

        public void OnWork(AnimCharBObjConfig data)
        {
            animData = data;
            PlayAnim();
        }
    }
}
