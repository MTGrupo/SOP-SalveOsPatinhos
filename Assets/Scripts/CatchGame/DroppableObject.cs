using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CatchGame
{
    public class DroppableObject : MonoBehaviour
    {
        [SerializeField] bool isExtraDuck;
        [SerializeField] int score;
        public static event Action<bool, int> OnCautch;
        
        [SerializeField] SpriteRenderer sprite;
        [SerializeField] bool rotateSprite;
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Catcher")) return;
            
            OnCautch?.Invoke(isExtraDuck, score);
            Destroy();
        }
        
        void RotateSprite()
        {
            if (!rotateSprite) return;
            
            var randomRotation = Random.Range(0, 360);
            sprite.transform.rotation = Quaternion.Euler(0, 0, randomRotation);
        }
        
        void Destroy()
        {
            Destroy(gameObject);
        }
        
        void OnDestroy()
        {
            CGTimer.OnTimeOver -= Destroy;
        }

        void Start()
        {
            CGTimer.OnTimeOver += Destroy;
            RotateSprite();
        }
    }
}