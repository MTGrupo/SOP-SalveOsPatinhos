using GameEnd;
using Leaderboard;
using Serialization;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour, ISerializable
{
    [SerializeField] TextMeshProUGUI timerText;

    private float elapsedTime = 0f;
    private bool isPaused = false;
    
    void Update()
    {
        if (isPaused) return;

        elapsedTime += Time.deltaTime;
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
    
    void SetPause(bool pause)
    {
        isPaused = pause;

        if (!pause) return;
        GameManager.SaveGameData();
    }

    void OnDestroy()
    {
        GameManager.OnGamePaused -= SetPause;
        EndPoint.OnGameEnded -= SendScore;
        
        GameManager.Unsubscribe(this);
    }
    
    async void SendScore()
    {
        if (!LeaderboardManager.Instance) return;
        await LeaderboardManager.Instance.SubmitScore(elapsedTime, "menor_tempo");
    }
    
    void Start()
    {
        GameManager.Subscribe(this);
        GameManager.OnGamePaused += SetPause;
        EndPoint.OnGameEnded += SendScore;
    }

    public void Save(SaveData data)
    {
        data.gameTimer = elapsedTime;
    }

    public void Load(SaveData data)
    {
        elapsedTime = data.gameTimer;
    }
}