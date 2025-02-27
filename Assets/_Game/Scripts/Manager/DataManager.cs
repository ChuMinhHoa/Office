using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.Storage;
using MemoryPack;
using Sirenix.OdinInspector;

public class DataManager : Singleton<DataManager>
{
    [field: SerializeField] public InGameData InGameData { get; private set; } = new();
    
    protected override void Awake()
    {
        base.Awake();
        LoadData();
    }
    
#if UNITY_EDITOR
    [Button]
#endif
    public void SaveData()
    {
        ObscuredPrefs.Set(GameStaticData.KeyInGameData, MemoryPackSerializer.Serialize(InGameData));
    }
    
#if UNITY_EDITOR
    [Button]
#endif
    public void LoadData()
    {
        InGameData = MemoryPackSerializer.Deserialize<InGameData>(
            ObscuredPrefs.Get<byte[]>(GameStaticData.KeyInGameData,
                MemoryPackSerializer.Serialize(new InGameData())));
    }
    
#if UNITY_EDITOR
    [Button]
#endif
    public void ResetData()
    {
        // Reset the in-memory data
        InGameData = new InGameData();
    
        // Remove the saved data from ObscuredPrefs
        ObscuredPrefs.DeleteKey(GameStaticData.KeyInGameData);
    }
}

public static class GameStaticData
{
    public const string KeyInGameData = "InGameData";
}

[System.Serializable]
[MemoryPackable]
public partial class InGameData
{
    
}
