using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.Cinemachine;
using UnityEngine;

namespace DefaultNamespace.Inventory
{
    public class SlotManager : MonoBehaviour
    {
        private const string SlotDataKey = "SlotData";
        
        
        public static void SaveSlotData(List<Slot> slots)
        {
            List<SlotData> slotDataList = new();

            foreach (var slot in slots)
            {
                if (slot.isSlotBusy)
                {
                    SlotData data = new SlotData
                    {
                        slotName = slot.name,
                        objectName = slot.nameIcon.text,
                        amount = slot.amountValue,
                    };
                    
                    slotDataList.Add(data);
                }
            }
            
            string json = JsonUtility.ToJson(new SlotDataWrapper { slots = slotDataList });
            PlayerPrefs.SetString(SlotDataKey, json);
            PlayerPrefs.Save();
        }
        
        public static List<SlotData> LoadSlotData()
        {
            if (!PlayerPrefs.HasKey(SlotDataKey)) return new List<SlotData>();
            string json = PlayerPrefs.GetString(SlotDataKey);
            SlotDataWrapper wrapper = JsonUtility.FromJson<SlotDataWrapper>(json);
            return wrapper?.slots ?? new List<SlotData>();
        }

        public static bool RemoveItemFromInvetory(string itemName, int quantity)
        {
            List<SlotData> slots = LoadSlotData();
            
            SlotData slot = slots.FirstOrDefault(s => s.objectName == itemName);

            if (slot == null || slot.amount < quantity)
            {
                return false;
            }
            
            slot.amount -= quantity;

            if (slot.amount == 0)
            {
                slots.Remove(slot);
            }
            
            string json = JsonUtility.ToJson(new SlotDataWrapper { slots = slots });
            PlayerPrefs.SetString(SlotDataKey, json);
            PlayerPrefs.Save();

            Debug.Log($"{quantity} {itemName}(s) removidos do inventário.");
            UpdateInventoryUI();
            return true;
        }
        
        public static SlotData GetSlotDataByName(string slotName)
        {
            List<SlotData> slots = LoadSlotData();
            return slots.FirstOrDefault(slot => slot.slotName == slotName);
        }
        
        public static void UpdateInventoryUI()
        {
            List<SlotData> slots = LoadSlotData();
            foreach (var slot in UnityEngine.Object.FindObjectsOfType<Slot>()) 
            {
                SlotData slotData = slots.FirstOrDefault(s => s.slotName == slot.name);
                if (slotData != null)
                {
                    slot.amountValue = slotData.amount;
                    slot.nameIcon.text = slotData.objectName;
                }
                else
                {
                    slot.amountValue = 0;
                }
            }
        }
        
        
        [Serializable]
        public class SlotData
        {
            public string slotName;
            public string objectName;
            public int amount;
        }
        
        [Serializable]
        public class SlotDataWrapper
        {
            public List<SlotData> slots;
        }
    }
}