using System.Collections.Generic;

namespace _Game.Scripts.Etc
{
    public static class MyCache
    {
        public static readonly List<string> StrAnimIndex = new();
        
        static MyCache()
        {
            StrAnimIndex.Add("AnimIndex0");
            StrAnimIndex.Add("AnimIndex1");
            StrAnimIndex.Add("AnimIndex2");
        }
    }
}
