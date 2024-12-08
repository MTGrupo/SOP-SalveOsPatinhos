using System;
using Interaction;
using UnityEngine;

namespace Dialogos.ObjectsOfDialogos
{
    public abstract class ObjectBase : InteractableObject, IInteraction
    {
        [SerializeField] private GameObject obj;
        [SerializeField] private GameObject iconeInteracao;
        
        private Rigidbody2D rb;
        private Collider2D colisor;
        public void Start()
        {
            obj.SetActive(false);
            rb = obj.GetComponent<Rigidbody2D>();
            colisor = obj.GetComponent<Collider2D>();
            AddObject(colisor, this);    
        }

        public virtual void OnPlayerInteraction()
        {
            Debug.Log("Interagindo com o objeto");
            obj.SetActive(false);
            RemoveObject(colisor);
        }
    }
}