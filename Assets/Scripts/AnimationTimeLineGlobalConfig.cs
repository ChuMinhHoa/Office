using System.Collections.Generic;
using AnimationController;
using Etc;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationTimeLineConfig", menuName = "GlobalConfig/AnimationTimeLineConfig")]
[GlobalConfig("Assets/Resources/GlobalConfig/")]
public class AnimationTimeLineGlobalConfig : ScriptableObject
{
    public List<AnimationDataConfig> animationDataConfigs = new();
   
#if UNITY_EDITOR
    [Button(50)]
    private void SetupAnimationData()
    {
        foreach (var anim in animationDataConfigs)
        {
            anim.SetupAnimationData();
        }
    }
#endif
    
    public float GetTimeAnimation<T>(T animPlayerLayer,int animID)
    {
        foreach (var animationData in animationDataConfigs)
        {
            if (animationData.animLayer.Equals(animPlayerLayer))
            {
                return animationData.GetAnimTime(animID);
            }
        }

        return 0;
    }
}

[System.Serializable]
public class AnimationDataConfig
{
    public AnimPlayerLayer animLayer;
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