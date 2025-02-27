using System.Collections.Generic;
using _Game.Scripts;
using MemoryPack;
using UnityEngine;

namespace _Game.Scripts
{
    [System.Serializable]
    [MemoryPackable]
    public partial class ItemDataSave
    {
        //public static ItemDataSave Instance => DataManager.Instance.InGameData.ItemDataSave;
        public List<ItemData> itemDataSaves = new();
    }

    [System.Serializable]
    [MemoryPackable]
    public partial class ItemData
    {
        [MemoryPackOrder(0)]
        [field: SerializeField] public _Game.Scripts.Etc.ReactiveValue<int> ID = new();
        [MemoryPackOrder(1)]
        [field: SerializeField] public _Game.Scripts.Etc.ReactiveValue<int> Bite = new();
    }
}


public partial class InGameData
{
    [MemoryPackOrder(0)]
    [field: SerializeField] public ItemDataSave ItemDataSave { get; set; } = new();
}