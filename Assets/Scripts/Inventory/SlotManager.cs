using System;
using System.Collections.Generic;
using System.Linq;
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

        public static bool RemoveItemFromInventory(string itemName, int quantity)
        {
            List<SlotData> slots = LoadSlotData();

            SlotData slotData = slots.FirstOrDefault(s => s.objectName == itemName);
            
            if (slotData == null || slotData.amount < quantity)
            {
                return false;
            }

            slotData.amount -= quantity;
            
            if (slotData.amount == 0)
            {
                slots.Remove(slotData);
            }
            
            
            string json = JsonUtility.ToJson(new SlotDataWrapper { slots = slots });
            PlayerPrefs.SetString(SlotDataKey, json);
            PlayerPrefs.Save();
            return true;
        }
        

        public static SlotData GetSlotDataByName(string itemName)
        {
            List<SlotData> slots = LoadSlotData();
            return slots.FirstOrDefault(slot => slot.objectName == itemName);
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
