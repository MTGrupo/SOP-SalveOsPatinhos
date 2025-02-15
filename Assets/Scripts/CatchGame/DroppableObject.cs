using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CatchGame
{
    public class DroppableObject : MonoBehaviour
    {
        [SerializeField] GameObject parent;
        
        [SerializeField] bool isExtraDuck;
        [SerializeField] int score;
        public static event Action<bool, int> OnCautch;
        
        [SerializeField] SpriteRenderer sprite;
        [SerializeField] bool rotateSprite;
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Catcher")) return;
            
            OnCautch?.Invoke(isExtraDuck, score);
            DestroyObject();
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if(CatchGame.Instance.Limit.bounds.Contains(transform.position)) return;
            
            DestroyObject();
        }

        void RotateSprite()
        {
            if (!rotateSprite) return;
            
            var randomRotation = Random.Range(0, 360);
            sprite.transform.rotation = Quaternion.Euler(0, 0, randomRotation);
        }
        
        void DestroyObject()
        {
            Destroy(parent);
        }
        
        void OnDestroy()
        {
            CGTimer.OnTimeOver -= DestroyObject;
        }

        void Start()
        {
            CGTimer.OnTimeOver += DestroyObject;
            RotateSprite();
        }
    }
}