using TMPro;
using UnityEngine;

namespace CatchGame
{
    public class CGScore : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
        
        [SerializeField] string _addScoreTrigger;
        [SerializeField] string _removeScoreTrigger;

        [SerializeField] private Animator animator;
        
        int totalScore;
        public int extraDucks;

        public int TotalScore
        {
            get => totalScore;
            private set
            {
                totalScore = value;
                
                scoreText.text = totalScore.ToString();
            }
        }
        
        private void AddScore(bool isExtraDuck, int score)
        {
            if (isExtraDuck)
            {
                extraDucks++;
                return;
            }
            
            totalScore += score;
            scoreText.text = totalScore.ToString();

            if (score >= 0)
            {
                OnScoreAdded();
                return;
            }
            
            OnScoreRemoved();
        }

        void ClearScore()
        {
            totalScore = 0;
            extraDucks = 0;
        }

        void OnScoreAdded()
        {
            animator.SetTrigger(_addScoreTrigger);
        }

        void OnScoreRemoved()
        {
            animator.SetTrigger(_removeScoreTrigger);
        }
        
        private void OnDestroy()
        {
            CatchGame.OnGameStarted -= ClearScore;
            DroppableObject.OnCautch -= AddScore;
        }

        private void Start()
        {
            CatchGame.OnGameStarted += ClearScore;
            DroppableObject.OnCautch += AddScore;
        }
    }
}