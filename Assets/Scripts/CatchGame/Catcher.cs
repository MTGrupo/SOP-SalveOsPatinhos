using UnityEngine;

namespace CatchGame
{
    public class Catcher : MonoBehaviour
    {
        [SerializeField] string _catchTrigger;
        [SerializeField] Animator animator;
        
        bool isDragging = false;

        void Update()
        {
            if (!isDragging) return;

            var mousePosition = GetMousePosition();
            
            if(!CatchGame.Instance.Limit.bounds.Contains(mousePosition)) return;
            
            transform.position = new Vector3(mousePosition.x, transform.position.y, transform.position.z);
        }
        
        void OnMouseDown()
        {
            if(!enabled) return;
            
            isDragging = true;
        }
        
        void OnMouseUp()
        {
            if(!enabled) return;
            
            isDragging = false;
        }
        
        Vector3 GetMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
        void OnCautch()
        {
            animator.SetTrigger(_catchTrigger);
        }
        
        void OnDestroy()
        {
            DroppableObject.OnCautch += (_, _) => OnCautch();
        }

        void Start()
        {
            DroppableObject.OnCautch += (_, _) => OnCautch();
        }
    }
}