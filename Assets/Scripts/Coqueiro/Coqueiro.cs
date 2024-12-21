using Assets.Scripts.Dialogos.Modal;
using Dialogos;
using Interaction;
using UnityEngine;

namespace Coqueiro
{
    public class Coqueiro : InteractableObject, IInteraction
    {
        private Rigidbody2D rb;
        private Collider2D colisor;
        
        [SerializeField] private DialogoBase dialogoBase;
        [SerializeField] private DialogoObject dialogObject;
        
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            colisor = GetComponent<Collider2D>();
            AddObject(colisor, this);
            dialogoBase.SetDialogoObject(dialogObject);
        }
        
        public void OnPlayerInteraction()
        {
             dialogoBase.StartDialogo();
        }
    }
}