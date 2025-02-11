using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private Button buttonInventory;
        [SerializeField] private List<Slot> slots = new();
        
        private Color defaultColor;
        
        private void Start()
        {
            inventoryPanel.SetActive(false);
            defaultColor = buttonInventory.image.color;
            buttonInventory.onClick.AddListener(ToggleInventory);
            LoadAndUpdateSlots();
        }

        private void LoadAndUpdateSlots()
        {
            List<SlotManager.SlotData> loadedData = SlotManager.LoadSlotData();
    
            foreach (var slotData in loadedData)
            {
                Slot slot = slots.FirstOrDefault(s => s.name == slotData.slotName);
                if (slot != null)
                {
                    slot.AddItemToSlot(slotData.objectName.ToLower() + ".png");
                    slot.amountValue = slotData.amount;
                }
            }
        }

        
        private void ToggleInventory()
        {
            bool isActive = inventoryPanel.activeSelf;
            inventoryPanel.SetActive(!isActive);
            buttonInventory.image.color = isActive ? defaultColor : defaultColor;
        }
        
#if UNITY_EDITOR
        private void Reset()
        {
            InitializeSlots();
        }
        
        private void InitializeSlots()
        {
            slots.Clear();
            slots.AddRange(FindObjectsOfType<Slot>());
            slots.Sort(CompareSlotNames);
        }
        
        private int CompareSlotNames(Slot a, Slot b)
        {
            int nA = ExtractNumber(a.name);
            int nB = ExtractNumber(b.name);
            return nA.CompareTo(nB);
        }

        private int ExtractNumber(string name)
        {
            string numString = Regex.Match(name, @"\d+").Value;
            return int.TryParse(numString, out int number) ? number : 0;
        }
#endif
        
        
    }
}