using UnityEngine;
using Action = System.Action;

namespace GameEnd
{
    public class EndPoint : MonoBehaviour
    {
        public static event Action OnGameEnded;
        
        void OnTriggerEnter2D(Collider2D other)
        {
            OnGameEnded?.Invoke();
            GameManager.LoadCredits();
        }
    }
}