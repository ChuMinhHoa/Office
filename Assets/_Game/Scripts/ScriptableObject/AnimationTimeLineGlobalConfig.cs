using System;
using System.Collections.Generic;
using _Game.Scripts.AnimationController;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Game.Scripts.ScriptableObject
{
    [CreateAssetMenu(fileName = "AnimationTimeLineConfig", menuName = "GlobalConfig/AnimationTimeLineConfig")]
    [GlobalConfig("Assets/Resources/GlobalConfig/")]
    public class AnimationTimeLineGlobalConfig : GlobalConfig<AnimationTimeLineGlobalConfig>
    {
        [FormerlySerializedAs("animationDataConfigs")] public List<AnimationDataConfig> animationCharacterDataConfigs = new();
        public List<AnimDefine> animDefines = new(0);
        
#if UNITY_EDITOR
        [Button(50)]
        private void SetupAnimationData()
        {
            foreach (var anim in animationCharacterDataConfigs)
            {
                anim.SetupAnimationData();
            }
        }
#endif
    
        public float GetTimeAnimation<T>(T animPlayerLayer,int animID)
        {
            foreach (var animationData in animationCharacterDataConfigs)
            {
                if (animationData.animLayer.Equals(animPlayerLayer))
                {
                    return animationData.GetAnimTime(animID);
                }
            }

            return 0;
        }

        public int GetAnimID(AnimCharacterLayer layer, CharState state, bool isOtherBase)
        {
            for (var i = 0; i < animDefines.Count; i++)
            {
                if (animDefines[i].layer != layer || animDefines[i].charState != state) continue;
                
                if (animDefines[i].animIndex.Count == 0)
                    return animDefines[i].baseAnimIndex;
                return isOtherBase
                    ? animDefines[i].animIndex[Random.Range(0, animDefines[i].animIndex.Count)]
                    : animDefines[i].baseAnimIndex;
            }

            return -1;
        }

        public int GetAnimLoopMax(AnimCharacterLayer currentAnimLayer, CharState charState)
        {
            Debug.Log(currentAnimLayer+" state "+charState);
            for (var i = 0; i < animDefines.Count; i++)
            {
                if (animDefines[i].layer != currentAnimLayer || animDefines[i].charState != charState) continue;
                Debug.Log(animDefines[i].totalTimeInBase);
                return animDefines[i].totalTimeInBase;
            }

            return -1;
        }
    }

    [System.Serializable]
    public class AnimationDataConfig
    {
        public AnimCharacterLayer animLayer;
        public List<AnimData> animData = new();
        public void SetupAnimationData()
        {
            var id = 0;
            foreach (var anim in animData)
            {
                if (anim.animationClips.Count == 0)
                    continue;
                anim.id = id;
                anim.animName = anim.animationClips[0].name;
                anim.GetTotalTimeAnim();
                id++;
            }
        }
    
        public float GetAnimTime(int animID)
        {
            foreach (var anim in animData)
            {
                if (anim.id == animID)
                {
                    return anim.totalTime;
                }
            }
            return 0;
        }
    }

    [System.Serializable]
    public class AnimData
    {
        public int id;
        public string animName;
        public float totalTime;
        public List<AnimationClip> animationClips = new();

        public void GetTotalTimeAnim()
        {
            totalTime = 0f;
            foreach (var anim in animationClips)
            {
                totalTime += anim.length;
            }
        }
    }

    [System.Serializable]
    public class AnimDefine
    {
        public AnimCharacterLayer layer;
        public CharState charState;
        public int baseAnimIndex;
        public int totalTimeInBase;
        public List<int> animIndex = new();
    }

    public enum CharState
    {
        None = 0,
        Relax,
        Talk,
        Work
    }
}