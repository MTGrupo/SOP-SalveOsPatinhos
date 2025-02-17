using System;
using System.Threading.Tasks;
using PlayerAccount;
using UnityEngine;

namespace Player
{
    public class PlayerProfile : MonoBehaviour
    {
        public static PlayerProfile Instance { get; private set; }
        public event Action<string> OnPlayerNameChanged; 

        string playerName;
    
        public string PlayerName
        {
            get => playerName;
            private set
            {
                playerName = value;
            
                OnPlayerNameChanged?.Invoke(value);
            }
        }
    
        public async Task SetPlayerName(string name)
        {
            if (!AuthenticationManager.Instance.IsAuthenticated)
            {
                SavePlayerNameLocally(name);
                return;
            }

            var currentServerName = await AuthenticationManager.Instance.GetPlayerName(false);

            if (name == currentServerName)
            {
                SavePlayerNameLocally(name);
                return;
            }

            await AuthenticationManager.Instance.UpdatePlayerName(name);
            SavePlayerNameLocally(name);
        }

        void SavePlayerNameLocally(string name)
        {
            PlayerName = name;
            PlayerPrefs.SetString("playerName", name);
        }

        async void DefineNameAfterAuthentication(bool isSignedIn)
        {
            var name = GetLocalPlayerName();

            if (isSignedIn)
            {
                string serverName = await AuthenticationManager.Instance.GetPlayerName(false);
                name = string.IsNullOrEmpty(serverName) ? name : serverName;
            }

            await SetPlayerName(name);
        }
    
        string GetLocalPlayerName()
        {
            return PlayerPrefs.HasKey("playerName") ? PlayerPrefs.GetString("playerName") : "Jogador";
        }

        void OnDestroy()
        {
            AuthenticationManager.Instance.OnAuthenticationStateChanged -= DefineNameAfterAuthentication;
        }

        void Start()
        {
            AuthenticationManager.Instance.OnAuthenticationStateChanged += DefineNameAfterAuthentication;
        }

        void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            PlayerName = GetLocalPlayerName();
        }
    }
}