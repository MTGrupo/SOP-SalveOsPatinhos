using System;
using PlayerAccount;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public static PlayerProfile Instance { get; private set; }
    public event Action<string> OnPlayerNameChanged; 

    private string playerName;
    
    public string PlayerName
    {
        get => playerName;
        private set
        {
            playerName = value;
            
            OnPlayerNameChanged?.Invoke(value);
        }
    }
    
    public void SetPlayerName(string name)
    {
        if (!AuthenticationManager.Instance.IsAuthenticated)
        {
            PlayerName = name;
            return;
        }

        AuthenticationManager.Instance.UpdatePlayerName(name);
        PlayerName = AuthenticationManager.Instance.GetPlayerName().ToString();
    }

    void ImportUnityPlayerName(bool isSignedIn)
    {
        if (!isSignedIn) return;

        var playerName = AuthenticationManager.Instance.GetPlayerName();
        
        SetPlayerName(playerName.ToString());
    }


    private void Start()
    {
        AuthenticationManager.Instance.OnAuthenticationStateChanged += ImportUnityPlayerName;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

   
}