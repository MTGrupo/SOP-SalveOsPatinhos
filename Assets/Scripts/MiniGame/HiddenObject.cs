﻿using UnityEngine;

namespace MiniGame
{
    public class HiddenObject : MonoBehaviour
    {
        [SerializeField] private ClickableObject hiddenObject;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private AudioSource duckSound;
        
        private bool isCollected;
        private bool isSuperimposed;
        
        private static MiniGame miniGame => MiniGame.instance;
        
        public bool IsCollected
        {
            get => isCollected;
            private set
            {
                Debug.Log("IsCollected: " + value);
                isCollected = value;
                miniGame.CheckObjective(this);
                enabled = !isCollected;
                sprite.enabled = !isCollected;
            }
        }

        public bool IsSuperimposed
        {
            get => isSuperimposed;
            private set
            {
                isSuperimposed = value;
        
                if (value)
                {
                    hiddenObject.AlertSuperimposing();
                }
            }
        }
        
        private void OnMouseDown()
        {
            IsSuperimposed = hiddenObject.IsSuperimposed();
            
            Debug.Log("IsSuperimposed: " + IsSuperimposed);
            
            if (IsSuperimposed) return;
            
            duckSound.Play();
            IsCollected = true;
        }
        
#if UNITY_EDITOR
        private void Reset()
        {
            hiddenObject = GetComponent<ClickableObject>();
            duckSound = GetComponent<AudioSource>();
        }
#endif
    }
}