using UnityEngine;
using UnityEngine.UI;

namespace PlayerAccount
{
    public class UILogin : MonoBehaviour
    {
        [SerializeField] private Button loginButton;
        [SerializeField] private Button logoutButton;
        [SerializeField] private GameObject loginPainel;
        [SerializeField] private Button cancelLoginButton;
    
        private void OnEnable()
        {
            loginButton.onClick.AddListener(OnLogin);
            
            if (!loginPainel)
            {
                AuthenticationManager.Instance.OnAuthenticationStateChanged += UpdateButtonVisibility;
                logoutButton.onClick.AddListener(OnLogout);
                UpdateButtonVisibility(AuthenticationManager.Instance.IsAuthenticated);
                return;
            }
            
            cancelLoginButton.onClick.AddListener(DisableLoginPainel);
        }
    
        private async void OnLogin()
        {
            await AuthenticationManager.Instance.InitSignIn();
        }

        private void OnLogout()
        {
            AuthenticationManager.Instance.SignOut();
        }
        
        private void UpdateButtonVisibility(bool isSignedIn)
        {
            if (!loginPainel)
            {
                loginButton.gameObject.SetActive(!isSignedIn);
                logoutButton.gameObject.SetActive(isSignedIn);
                return;
            }

            if (isSignedIn)
            {
                DisableLoginPainel();
            }
        }

        private void DisableLoginPainel()
        {
            loginPainel.SetActive(false);
        }

        private void OnDisable()
        {
            AuthenticationManager.Instance.OnAuthenticationStateChanged -= UpdateButtonVisibility;
            loginButton.onClick.RemoveListener(OnLogin);
            
            if (!loginPainel)
            {
                logoutButton.onClick.RemoveListener(OnLogout);
                return;
            }
            
            cancelLoginButton.onClick.RemoveListener(DisableLoginPainel);
        }
    }
}