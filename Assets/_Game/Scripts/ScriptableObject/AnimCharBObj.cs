using System.Collections.Generic;
using _Game.Scripts.AnimationController;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace _Game.Scripts.ScriptableObject
{
    [CreateAssetMenu(fileName = "AnimCharBObjConfig", menuName = "GlobalConfig/AnimCharBObj")]
    [GlobalConfig("Assets/Resources/GlobalConfig/")]
    public class AnimCharBObj : GlobalConfig<AnimCharBObj>
    {
        public List<AnimCharBObjConfig> animCharBObjConfigs;

        public AnimCharBObjConfig GetData(int objID)
        {
            foreach (var data in animCharBObjConfigs)
            {
                if (data.id == objID)
                {
                    return data;
                }
            }
            return null;
        }
    }
    
    [System.Serializable]
    public class AnimCharBObjConfig
    {
        public int id;
        
        [BoxGroup("Char Anim")] public AnimCharacterLayer cLayer;
        [BoxGroup("Char Anim")] public int animID;
       
        [BoxGroup("Obj Anim")] public AnimObjLayer objLayer;
        [BoxGroup("Obj Anim")] public int animObjId;
    }
}
