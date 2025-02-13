using UnityEngine;

namespace PlayerAccount
{
    public class AuthenticationLog : MonoBehaviour
    {
        [Header("Componentes")]
        [SerializeField] private TMPro.TextMeshProUGUI logText;
        [SerializeField] private Animator animator;
        
        [Header("Animações")]
        [SerializeField]
        string _openParameter;
        
        public bool IsOpen
        {
            get => animator.GetBool(_openParameter);
            private set => animator.SetBool(_openParameter, value);
        }
        
        void UpdateLogText(string message)
        {
            logText.text = message;
        }
        
        async void ShowLog(bool isSignedIn)
        {
            Debug.Log(isSignedIn);
            if (isSignedIn)
            {
                string playerName = await AuthenticationManager.Instance.GetPlayerName(false);
                UpdateLogText("Bem-vindo(a) " + playerName + "!");
            }
            else
            {
                UpdateLogText("Conta desconectada.");
            }
            
            IsOpen = true;
            
            Invoke(nameof(CloseLog), 2);
        }
        
        void CloseLog()
        {
            IsOpen = false;
        }

        void OnDestroy()
        {
            AuthenticationManager.Instance.OnAuthenticationStateChanged -= ShowLog;
        }

        void Start()
        {
            AuthenticationManager.Instance.OnAuthenticationStateChanged += ShowLog;
        }
    }
}