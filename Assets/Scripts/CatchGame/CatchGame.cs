using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

        void ShowCurrentResults()
        {
            UpdateCurrentResults();
        }
        
        void UpdateCurrentResults()
        {
            UpdateDuckAmount();
            isFinished = true;
        }

        void StartGame()
        {
            isFinished = false;
            ducksRecued = 0;
            OnGameStarted?.Invoke();
        }
        
        void FinishGame()
        {
            SetResult(GetAvailableCatchGameID(), isFinished, ducksRecued);
            Instance = null;
            GameManager.LoadGame(true);
        }
        
        static void SetResult(int CatchGameID, bool isFinished, int ducksRecued)
        {
            results[CatchGameID] = new CatchGameResult { isFinished = isFinished, ducksRecued = ducksRecued };
        }

        public static CatchGameResult GetResult(int CatchGameID)
        {
            return results.ContainsKey(CatchGameID) ? results[CatchGameID] : new CatchGameResult();
        }
        
        int GetAvailableCatchGameID()
        {
            if (results.Count == 0) return 1;

            return results.Keys.Max() + 1;
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
            TutorialController.OnTutorialClosed -= StartGame;
        }

        void Start()
        {
            CGTimer.OnTimeOver += ShowCurrentResults;
            TutorialController.OnTutorialClosed += StartGame;
        }

        void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
    }
}