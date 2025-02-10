using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        [SerializeField] private List<Slot> slots = new();

        private void Start()
        {
            textMeshProUGUI.gameObject.SetActive(false);   
        }

        private void Update()
        {
            bool allSlotsEmpty = true;
            List<string> occupiedSlots = new();
            int availableSlotsCount = 0;  

            foreach (Slot slot in slots)
            {
                if (slot.IsSlotBusy())
                {
                    allSlotsEmpty = false;
                    slot.gameObject.SetActive(true);
                    occupiedSlots.Add(slot.gameObject.name);
                }
                else
                {
                    slot.gameObject.SetActive(false);
                    availableSlotsCount++;  
                }
            }
            
            if (allSlotsEmpty)
            {
                textMeshProUGUI.gameObject.SetActive(true);
                textMeshProUGUI.text = "Mochila vazia";
            }
            else
            {
                textMeshProUGUI.gameObject.SetActive(true);
                textMeshProUGUI.text = $"Slots disponíveis: {availableSlotsCount}";
            }
            
            Debug.Log(occupiedSlots.Count > 0
                ? $"Slots ocupados: {string.Join(", ", occupiedSlots)}"
                : "Nenhum slot ocupado");
        }



#if UNITY_EDITOR
        private void Reset()
        {
            slots.Clear();
            slots.AddRange(FindObjectsOfType<Slot>());
            
            slots.Sort((a, b) =>
            {
                int nA = ExtractNumber(a.name);
                int nB = ExtractNumber(b.name);
                return nA.CompareTo(nB);
            });
        }

        private int ExtractNumber(string name)
        {
            string numString = Regex.Match(name, @"\d+").Value;
            return int.TryParse(numString, out int number) ? number : 0;
        }
#endif
    }
}