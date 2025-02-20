using System;
using System.Collections.Generic;
using MiniGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace CatchGame
{
    public class CatchGame : MonoBehaviour
    {
        [field: SerializeField] public Collider Limit { get; private set; }
        
        public static CatchGame Instance { get; private set; }
        
        public static event Action OnGameStarted;
        
        [SerializeField] CGScore score;
        
        private static Dictionary<int, CatchGameResult> results = new ();
        
        bool isFinished;
        int ducksRecued;

        [SerializeField] GameObject resultsContent;
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI ducksRecuedText;
        [SerializeField] Button finishGameButton;
        [SerializeField] Button restartGameButton;
        
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip resultsSound;

        void EnableLimit()
        {
            Limit.enabled = true;
        }
        
        void DisableLimit()
        {
            Limit.enabled = false;
        }
        
        void ShowCurrentResults()
        {
            UpdateCurrentResults();
            resultsContent.SetActive(true);
            DisableLimit();
            audioSource.Play();
        }
        
        void UpdateCurrentResults()
        {
            UpdateDuckAmount();
            scoreText.text = score.TotalScore.ToString();
            ducksRecuedText.text = ducksRecued.ToString();
            isFinished = true;
        }

        void StartGame()
        {
            EnableLimit();
            isFinished = false;
            ducksRecued = 0;
            OnGameStarted?.Invoke();
        }
        
        void FinishGame()
        {
            SetResult(MiniGameSession.currentMiniGameID, isFinished, ducksRecued);
            Instance = null;
            GameManager.LoadGame(true);
        }
        
        static void SetResult(int catchGameID, bool isFinished, int ducksRecued)
        {
            results[catchGameID] = new CatchGameResult { isFinished = isFinished, ducksRecued = ducksRecued };
        }

        public static CatchGameResult GetResult(int catchGameID)
        {
            return results.ContainsKey(catchGameID) ? results[catchGameID] : new CatchGameResult();
        }
        
        void UpdateDuckAmount()
        {
            switch (score.TotalScore)
            {
                case >=1000:
                    ducksRecued += 9;
                    break;
                case >=750:
                    ducksRecued += 6;
                    break;
                case >=500:
                    ducksRecued += 3;
                    break;
            }

            if (score.extraDucks <= 0) return;
            
            ducksRecued += score.extraDucks;
        }

        public struct CatchGameResult
        {
            public bool isFinished;
            public int ducksRecued;
        }
        
        void OnDestroy()
        {
            CGTimer.OnTimeOver -= ShowCurrentResults;
            CGTimer.OnTimeOver -= DisableLimit;
            TutorialController.OnTutorialClosed -= StartGame;
            
            finishGameButton.onClick.RemoveListener(FinishGame);
            restartGameButton.onClick.RemoveAllListeners();
        }

        void Start()
        {
            CGTimer.OnTimeOver += ShowCurrentResults;
            CGTimer.OnTimeOver += DisableLimit;
            TutorialController.OnTutorialClosed += StartGame;
            
            finishGameButton.onClick.AddListener(FinishGame);
            restartGameButton.onClick.AddListener(StartGame);
            restartGameButton.onClick.AddListener(() => resultsContent.SetActive(false));
        }

        void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DisableLimit();
        }
    }
}