using TMPro;
using UnityEngine;

namespace CatchGame
{
    public class CGScore : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
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
        }
        
        private void OnDestroy()
        {
            DroppableObject.OnCautch -= AddScore;
        }

        private void Start()
        {
            CatchGame.OnGameStarted += () => TotalScore = 0;
            DroppableObject.OnCautch += AddScore;
        }
    }
}