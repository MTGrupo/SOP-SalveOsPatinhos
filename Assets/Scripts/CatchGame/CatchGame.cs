using System;
using UnityEngine;

namespace CatchGame
{
    public class CatchGame : MonoBehaviour
    {
        [field: SerializeField] public Collider Limit { get; private set; }
        
        public static CatchGame Instance { get; private set; }
        
        public static Action OnGameStarted;

        
        public static bool isFinished = false;
        public static int duckAmount;
        
        
        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
    }
}