using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationTools : MonoBehaviour
{
    public List<GameObject> modelImporters = new();
    public AnimatorController animator;
    [BoxGroup("Add animation")] public int layerIndex;
    [Button(50), BoxGroup("Add animation")]
    void AddAnimationClipToAnimator()
    {
        foreach (var modelImporter in modelImporters)
        {
            string path = AssetDatabase.GetAssetPath(modelImporter);
            ModelImporter model = AssetImporter.GetAtPath(path) as ModelImporter;
            if (model != null)
            {
                ModelImporterClipAnimation[] animations = model.clipAnimations;
                if (animations.Length > 0)
                {
                    AnimationClip clip = null;
                    Object[] assets = AssetDatabase.LoadAllAssetsAtPath(path);
                    foreach (Object asset in assets)
                    {
                        if (asset is AnimationClip && asset.name == animations[0].name)
                        {
                            clip = asset as AnimationClip;
                            break;
                        }
                    }
                    
                    if (clip != null)
                    {
                        AnimatorController controller = animator;
                        AnimatorStateMachine stateMachine = controller.layers[layerIndex].stateMachine;
                        AnimatorState state = stateMachine.AddState(clip.name);
                        state.motion = clip;
                        Debug.Log("Add animation clip to animator success");
                    }
                    else
                    {
                        Debug.Log("Fail"+path);
                    }
                }
            }
        }
    }
    [BoxGroup("Change Name Animation")] 
    [Button]
    void ChangeNameAnimation()
    {
        foreach (var modelImporter in modelImporters)
        {
            string path = AssetDatabase.GetAssetPath(modelImporter);
            ModelImporter model = AssetImporter.GetAtPath(path) as ModelImporter;
            if (model != null)
            {
                ModelImporterClipAnimation[] animations = model.clipAnimations;
                if (animations.Length > 0)
                {
                    animations[0].name = modelImporter.name;
                    animations[0].loopTime = true;
                    animations[0].lockRootRotation = true;
                    animations[0].lockRootHeightY = true;
                    animations[0].lockRootPositionXZ = true;
                    model.clipAnimations = animations;
                    model.SaveAndReimport();
                    Debug.Log("Change name animation success"+animations[0].name);
                }
                else
                {
                    Debug.Log("Fail"+path);
                }
            }
        }
    }
}
