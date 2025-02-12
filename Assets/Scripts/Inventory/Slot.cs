using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Inventory
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] public TextMeshProUGUI nameIcon;
        [SerializeField] private Image icon;
        [SerializeField] public TextMeshProUGUI amount;
        public bool isSlotBusy { get; private set; }
        
        private int _amount;
        public int amountValue { 
            get => _amount;
            set
            {
                _amount = Mathf.Max(1, value);
                amount.text = _amount > 0 ? _amount.ToString() : "";
                if (_amount == 0) ClearSlot();
            }
        }
        
        private string itemFileName;
        
        private void Awake()
        {
            SetSlotVisibility(false);
            isSlotBusy = false;
            amountValue = 1;
        }

        public void AddItemToSlot(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || isSlotBusy) return;
            
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
            if (!File.Exists(filePath)) return;
            
            nameIcon.text = Path.GetFileNameWithoutExtension(fileName);
            LoadImage(filePath);
            SetSlotVisibility(true);
            isSlotBusy = true;
            itemFileName = fileName;
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
        
        private void ClearSlot()
        {
            nameIcon.text = "";
            icon.sprite = null;
            isSlotBusy = false;
            SetSlotVisibility(false);
        }
        
        public bool ContainsItem(string itemFileName) => itemFileName == this.itemFileName;
        private void SetSlotVisibility(bool isVisible) => slotPrefab.SetActive(isVisible);

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