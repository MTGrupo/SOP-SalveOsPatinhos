using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DefaultNamespace.Inventory;
using Interaction;
using UnityEngine;

namespace Dialogos.ObjectsOfDialogos
{
    public abstract class ObjectBase : InteractableObject, IInteraction
    {
        [SerializeField] public GameObject obj;
        [SerializeField] private GameObject iconeInteracao;
        [SerializeField] private List<Slot> slots = new();
        
        private Rigidbody2D rb;
        private Collider2D colisor;
        
        void Awake()
        {
            obj = gameObject;
        }
        public virtual void Start()
        {
            obj.SetActive(false);
            rb = obj.GetComponent<Rigidbody2D>();
            colisor = obj.GetComponent<Collider2D>();
            AddObject(colisor, this);    
        }

        public virtual void OnPlayerInteraction()
        {
            obj.SetActive(false);
            RemoveObject(colisor);
            PlaceInFirstAvailableSlot();
        }

        private void PlaceInFirstAvailableSlot()
        {
            Slot existingSlot = slots.FirstOrDefault(slot => slot.namesInSlots.Contains(obj.name.ToLower()));

            if (existingSlot)
            {
                existingSlot.IncrecementAmount();
                existingSlot.amount.text = existingSlot.namesInSlots.Count.ToString();
            }
            else
            {
                Slot availableSlot = slots.FirstOrDefault(slot => !slot.IsSlotBusy());

                if (availableSlot)
                {
                    availableSlot.namesInSlots.Add(obj.name.ToLower());
                    availableSlot.IncrecementAmount();
                    availableSlot.amount.text = availableSlot.GetAmount().ToString();

                    AddItemToSlot(availableSlot);
                }
                else
                {
                    Debug.LogWarning("Nenhum slot disponível!");
                }
            }
        }

        
        private void AddItemToSlot(Slot availableSlot)
        {
            string itemFileName = obj.name.ToLower() + ".png";
            availableSlot.AddItemToSlot(itemFileName);
            availableSlot.DisplaySlotData();
            Debug.Log($"Objeto {obj.name}.png adicionado ao slot {availableSlot.name.ToLower()}");
        }
        
#if UNITY_EDITOR
        private void Reset()
        {
            obj = gameObject;
            iconeInteracao = GameObject.Find("Icone Interacao");
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