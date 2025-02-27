using System;
using System.Collections.Generic;
using AnimationController;
using Sirenix.OdinInspector;

namespace Etc
{
    public static class MyCache
    {
        public const string StrAnimIndex = "AnimIndex";
        public static readonly List<AnimLayerCache<AnimPlayerLayer>> AnimPlayerLayerCaches = new();
        public static readonly List<AnimLayerCache<AnimObjLayer>> AnimObjLayerCaches = new();
        
        public static void AddAnimTimeLine<T>(T animLayer, int animID, float time)
        {
            switch (animLayer)
            {
                case AnimPlayerLayer animPlayerLayer:
                    AddTimeLine(AnimPlayerLayerCaches, animPlayerLayer, animID, time);
                    break;
                case AnimObjLayer objAnimLayer:
                    AddTimeLine(AnimObjLayerCaches, objAnimLayer, animID, time);
                    break;
            }
        }

        private static void AddTimeLine<T>(List<AnimLayerCache<T>> listAnim, T animLayer, int animID, float time)
        {
            foreach (var layer in listAnim)
            {
                if (layer.AnimLayer.Equals(animLayer))
                {
                    layer.AddAnimLayer(animID, time);
                    return;
                }
            }
            
            var newLayer = new AnimLayerCache<T> {AnimLayer = animLayer};
            newLayer.AddAnimLayer(animID, time);
            listAnim.Add(newLayer);
        }

        public static float GetAnimTime<T>(int currentAnimIndex, T currentAnimLayer)
        {
            return currentAnimLayer switch
            {
                AnimPlayerLayer animPlayerLayer => AnimTime(AnimPlayerLayerCaches, animPlayerLayer, currentAnimIndex),
                AnimObjLayer objAnimLayer => AnimTime(AnimObjLayerCaches, objAnimLayer, currentAnimIndex),
                _ => 0
            };
        }
        
        private static float AnimTime<T>(List<AnimLayerCache<T>> anims, T animLayer, int currentAnimIndex)
        {
            foreach (var layer in anims)
            {
                if (layer.AnimLayer.Equals(animLayer))
                    return layer.GetAnimLayer(currentAnimIndex);
            }
            return 0;
        }
        
        public static bool IsHaveAnim<T>(T animLayer, int animID)
        {
            return animLayer switch
            {
                AnimPlayerLayer playerLayer => HaveAnim(AnimPlayerLayerCaches, playerLayer, animID),
                AnimObjLayer objLayer => HaveAnim(AnimObjLayerCaches, objLayer, animID),
                _ => false
            };
        }
        
        private static bool HaveAnim<T>(List<AnimLayerCache<T>> anims, T animLayer, int animID)
        {
            foreach (var layer in anims)
            {
                if (layer.AnimLayer.Equals(animLayer))
                    return layer.animCache.ContainsKey(animID);
            }
            return false;
        }
    }
    
    public class AnimLayerCache<T>
    {
        public T AnimLayer;
        [ShowInInspector] public Dictionary<int, float> animCache = new();
        
        public void AddAnimLayer(int animID, float time)
        {
            animCache.TryAdd(animID, time);
        }
        
        public float GetAnimLayer(int animID)
        {
            return animCache.GetValueOrDefault(animID);
        }
    }
}
