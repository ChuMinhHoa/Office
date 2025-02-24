using System.Collections.Generic;
using MemoryPack;
using UnityEngine;

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
    [field: SerializeField] public ReactiveValue<int> ID = new();
    [MemoryPackOrder(1)]
    [field: SerializeField] public ReactiveValue<int> Bite = new();
}

public partial class InGameData
{
    [MemoryPackOrder(0)]
    [field: SerializeField] public ItemDataSave ItemDataSave { get; set; } = new();
}