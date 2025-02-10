using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Inventory
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private TextMeshProUGUI nameIcon;
        [SerializeField] private Image icon;
        [SerializeField] public TextMeshProUGUI amount;
        private bool isSlotBusy;
        private string itemName; 
        private int itemAmount;
        public readonly List<string> namesInSlots = new();
        
        private void Awake()
        {
            SetSlotVisibility(false); 
            itemAmount = 0; 
        }

        public void AddItemToSlot(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return;
            
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

            if (!File.Exists(filePath)) return;
            
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            
            ProcessItem(fileNameWithoutExtension, filePath);
        }

        private void ProcessItem(string itemNameWithoutExtension, string filePath)
        {
            itemName = itemNameWithoutExtension;
            itemAmount = 1;
            amount.text = itemAmount.ToString();
            nameIcon.text = itemNameWithoutExtension;
            LoadImage(filePath);
            SetSlotVisibility(true);
            MarkSlotAsBusy();
        }

        public void DisplaySlotData()
        {
            Debug.Log($"Slot: {nameIcon.text}, Item: {itemName}, Quantity: {itemAmount}");
        }
        
        private void MarkSlotAsBusy()
        {
            if (!isSlotBusy)
            {
                isSlotBusy = true;
            }
        }

        private void LoadImage(string path)
        {
            byte[] fileData = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(fileData))
            {
                icon.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }
        
        public void RemoveItem()
        {
            isSlotBusy = false;
            nameIcon.text = "";
            icon.sprite = null;
            itemName = "";  
            itemAmount = 0;  
            SetSlotVisibility(false);
        }
        
        private void SetSlotVisibility(bool isVisible) => slotPrefab.SetActive(isVisible);
        public bool IsSlotBusy() => isSlotBusy;
        public int GetAmount() => itemAmount;
        public void IncrecementAmount() => itemAmount += 1;
        

#if UNITY_EDITOR
        private void Reset()
        {
            slotPrefab = gameObject;
            nameIcon = GetComponentInChildren<TextMeshProUGUI>();
            icon = GetComponentInChildren<Image>();
            amount = GetComponentInChildren<TextMeshProUGUI>();
        }
#endif
    }
}