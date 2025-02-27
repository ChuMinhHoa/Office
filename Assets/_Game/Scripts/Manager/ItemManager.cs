using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using _Game.Scripts.Patern;
using UnityEngine;

namespace _Game.Scripts.Manager
{
    public class ItemManager : Singleton<ItemManager>
    {
        public List<ItemData> itemDatas = new();

        private void Start()
        {
            InitData();
        }

        void InitData()
        {
            itemDatas = DataManager.Instance.InGameData.ItemDataSave.itemDataSaves;
            for (int i = 0; i < itemDatas.Count; i++)
            {
                itemDatas[i].ID.ReactiveProperty.Skip(1).Subscribe(OnChangeID);
            }
        }

        void OnChangeID(int id)
        {
            Debug.Log($"item change value {id}");
        }
    }
}
