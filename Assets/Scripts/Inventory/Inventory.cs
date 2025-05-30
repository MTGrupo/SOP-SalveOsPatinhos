﻿using System;
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
        [SerializeField] private Button closeInventoryButton;
        
        private void Start()
        {
            inventoryPanel.SetActive(false);
            buttonInventory.onClick.AddListener(ToggleInventory);
            closeInventoryButton.onClick.AddListener(CloseInventory);
            LoadAndUpdateSlots();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                ToggleInventory();
            }
        }

        private void LoadAndUpdateSlots()
        {
            List<SlotData> loadedData = SlotManager.LoadSlotData();
    
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
            if (!isActive)
            {
                LoadAndUpdateSlots();
            }
        }

        private void CloseInventory()
        {
            inventoryPanel.SetActive(false);
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