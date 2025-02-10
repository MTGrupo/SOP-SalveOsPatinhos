using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private Button buttonInventory;
        
        private Color defaultColor;
        
        private void Start()
        {
            inventoryPanel.SetActive(false);
            defaultColor = buttonInventory.image.color;
            buttonInventory.onClick.AddListener(ToggleInventory);
        }

        private void ToggleInventory()
        {
            bool isActive = inventoryPanel.activeSelf;
            inventoryPanel.SetActive(!isActive);
            buttonInventory.image.color = isActive ? defaultColor : defaultColor;
        }
        
    }
}