using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public static PlayerProfile Instance { get; private set; }
    public string PlayerName { get; private set; }
    
    public void SetPlayerName(string name)
    {
        PlayerName = name;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

   
}