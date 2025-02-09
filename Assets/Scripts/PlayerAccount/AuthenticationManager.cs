using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;

namespace PlayerAccount
{
    public class AuthenticationManager : MonoBehaviour
    {
        public static AuthenticationManager Instance { get; private set; }
        
        public bool IsAuthenticated => AuthenticationService.Instance.IsSignedIn;
        
        public event Action<bool> OnAuthenticationStateChanged;
        
        async Task InitializeServicesAsync()
        {
            try
            {
                await UnityServices.InitializeAsync();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
        async void SignedIn()
        {
            try
            {
                var accessToken = PlayerAccountService.Instance.AccessToken;
                await SignInWithUnityAsync(accessToken);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }
    
        async Task SignInWithUnityAsync(string accessToken)
        {
            try
            {
                await Task.Delay(100);
                await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);
                
                OnAuthenticationStateChanged?.Invoke(IsAuthenticated);
            }
            catch (AuthenticationException ex)
            {
                Debug.LogException(ex);
            }
            catch (RequestFailedException ex)
            {
                Debug.LogException(ex);
            }
        }

        public async Task InitSignIn()
        {
            if (IsAuthenticated) return;
            
            try
            {
                await PlayerAccountService.Instance.StartSignInAsync();
            }
            catch (RequestFailedException ex)
            {
                Debug.LogException(ex);
            }
        }
        
        public void SignOut()
        {
            if (!IsAuthenticated) return;
            
            try
            {
                AuthenticationService.Instance.SignOut();
                PlayerAccountService.Instance.SignOut();
                AuthenticationService.Instance.ClearSessionToken();
                
                OnAuthenticationStateChanged?.Invoke(IsAuthenticated);
            }
            catch (Exception e)
            {
                Debug.LogError($"Unexpected error during logout: {e.Message}");
            }
        }

        public async Task DeleteAccount()
        {
            try
            {
                await AuthenticationService.Instance.DeleteAccountAsync();
                
                OnAuthenticationStateChanged?.Invoke(IsAuthenticated);
            }
            catch (AuthenticationException ex)
            {
                Debug.LogException(ex);
            }
        }

        public async Task UpdatePlayerName(string name)
        {
            if (!IsAuthenticated) return;
            
            try
            {
                await AuthenticationService.Instance.UpdatePlayerNameAsync(name);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }
        
        public async Task<string> GetPlayerName(bool containsTag)
        {
            if (!IsAuthenticated) return null;
            
            try
            {
                string playerName = await AuthenticationService.Instance.GetPlayerNameAsync();
                
                if (!containsTag) return playerName.Split('#')[0];
                
                return playerName;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }
        
        async Task Reconnect()
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                
                OnAuthenticationStateChanged?.Invoke(IsAuthenticated);
            }
            catch (RequestFailedException ex)
            {
                Debug.LogException(ex);
            }
        }

        void OnDestroy()
        {
            PlayerAccountService.Instance.SignedIn -= SignedIn;
        }

        async void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            await InitializeServicesAsync();
            
            PlayerAccountService.Instance.SignedIn += SignedIn;
            
            if (!AuthenticationService.Instance.SessionTokenExists) return;
            
            await Reconnect();
        }
    }
}