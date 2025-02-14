using System;
using TMPro;
using UnityEngine;

namespace CatchGame
{
    public class CGTimer : MonoBehaviour
    {
        public static event Action OnTimeOver;
        
        [SerializeField] TextMeshProUGUI timerText;
        
        float currentTime = 30;
        bool isPaused = true;

        private void Update()
        {
            if (isPaused) return;
            
            if(currentTime < 1)
            {
                isPaused = true;
                OnTimeOver?.Invoke();
                return;
            }

            if (currentTime < 10)
            {
                timerText.color = Color.red;
            }
            
            currentTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        
        void UpdateTimerUI()
        {
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = $"{seconds:0}";
        }
        
        void SetPause(bool pause)
        {
            isPaused = pause;
        }
        
        void StartTimer()
        {
            currentTime = 30;
            isPaused = false;
        }
        
        void OnDestroy()
        {
            GameManager.OnGamePaused -= SetPause;
            CatchGame.OnGameStarted -= StartTimer;
        }

        void Start()
        {
            GameManager.OnGamePaused += SetPause;
            CatchGame.OnGameStarted += StartTimer;
        }
    }
}