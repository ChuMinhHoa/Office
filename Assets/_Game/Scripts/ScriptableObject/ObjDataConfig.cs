using System.Collections.Generic;
using _Game.Scripts.Objects;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace _Game.Scripts.ScriptableObject
{
    [CreateAssetMenu(fileName = "ObjDataConfig", menuName = "GlobalConfig/ObjDataConfig")]
    [GlobalConfig("Assets/Resources/GlobalConfig/")]
    public class ObjDataConfig : GlobalConfig<ObjDataConfig>
    {
        public List<ObjConfig> objConfigs = new();
        
        #if UNITY_EDITOR
        [Button]
        private void ReloadID()
        {
            for (var i = 0; i < objConfigs.Count; i++)
            {
                objConfigs[i].objID = i;
                Debug.Log($"{objConfigs[i].name} change id to {i}");
            }
        }
        #endif
        public ObjConfig GetConfig(ObjType objType)
        {
            foreach (var data in objConfigs)
            {
                if (data.type == objType)
                {
                    return data;
                }
            }

            return null;
        }
    }
}
