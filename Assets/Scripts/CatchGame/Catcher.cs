using System;
using UnityEngine;

namespace CatchGame
{
    public class Catcher : MonoBehaviour
    {
        [SerializeField] string _catchTrigger;
        [SerializeField] Animator animator;
        
        bool isDragging = false;
        Vector2 dragOffset;

        void Update()
        {
            if (!isDragging) return;

            var mousePosition = GetMousePosition();
            mousePosition.z = transform.position.z;
            
            if(!CatchGame.Instance.Limit.bounds.Contains(mousePosition)) return;
            
            transform.position = new Vector3(mousePosition.x - dragOffset.x, transform.position.y, transform.position.z);
        }
        
        void OnMouseDown()
        {
            if(!enabled) return;
            
            isDragging = true;
            dragOffset = GetMousePosition() - transform.position;
        }
        
        void OnMouseUp()
        {
            if(!enabled) return;
            
            isDragging = false;
        }

        private void ResetPlayerPosition()
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }

        Vector3 GetMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
        void OnCautch(bool isExtraDuck, int score)
        {
            animator.SetTrigger(_catchTrigger);
        }
        
        void OnDestroy()
        {
            DroppableObject.OnCautch -= OnCautch;
            CatchGame.OnGameStarted -= ResetPlayerPosition;
        }

        void Start()
        {
            CatchGame.OnGameStarted += ResetPlayerPosition;
            DroppableObject.OnCautch += OnCautch;
        }
    }
}