using _Game.Scripts.Objects;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace _Game.Scripts.ScriptableObject
{
    [CreateAssetMenu(fileName = "ObjConfig", menuName = "ScriptableObject/ObjConfig")]
    public class ObjConfig : UnityEngine.ScriptableObject
    {
        [HideLabel, PreviewField(150)]public Sprite objSprite;
        public int objID;
        public ObjType type;
        public ObjStaticType staticType;
        public string objName;
        public int animWorkID;
    }
}
