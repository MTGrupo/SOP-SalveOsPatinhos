using TMPro;
using UnityEngine;

namespace MiniGame
{
    public class GoalMessage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI messageText;
        
        void Start()
        {
            MiniGame.onMessageUpdated += UpdateMessage;
        }

        void OnDisable()
        {
            MiniGame.onMessageUpdated -= UpdateMessage;
        }
        
        void UpdateMessage(string message)
        {
            messageText.text = message;
        }
        
#if UNITY_EDITOR
        private void Reset()
        {
            messageText = GetComponent<TextMeshProUGUI>();
        }
#endif
    }
}