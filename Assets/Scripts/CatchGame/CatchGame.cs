using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CatchGame
{
    public class CatchGame : MonoBehaviour
    {
        [field: SerializeField] public Collider Limit { get; private set; }
        
        public static CatchGame Instance { get; private set; }
        
        public static Action OnGameStarted;
        
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
        
        void FinishGame()
        {
            SetResult(GetAvailableCatchGameID(), isFinished, ducksRecued);
            Instance = null;
            GameManager.LoadGame(true);
        }
        
        public static void SetResult(int CatchGameID, bool isFinished, int ducksRecued)
        {
            results[CatchGameID] = new CatchGameResult { isFinished = isFinished, ducksRecued = ducksRecued };
        }

        public static CatchGameResult GetResult(int CatchGameID)
        {
            return results.ContainsKey(CatchGameID) ? results[CatchGameID] : new CatchGameResult();
        }
        
        private int GetAvailableCatchGameID()
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
        
        private void OnDestroy()
        {
            CGTimer.OnTimeOver -= ShowCurrentResults;
        }

        private void Start()
        {
            CGTimer.OnTimeOver += ShowCurrentResults;
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