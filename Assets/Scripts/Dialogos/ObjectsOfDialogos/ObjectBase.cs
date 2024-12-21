using Interaction;
using UnityEngine;

namespace Dialogos.ObjectsOfDialogos
{
    public abstract class ObjectBase : InteractableObject, IInteraction
    {
        [SerializeField] public GameObject obj;
        [SerializeField] private GameObject iconeInteracao;
        
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
        }
    }
}