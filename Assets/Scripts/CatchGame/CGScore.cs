using TMPro;
using UnityEngine;

namespace CatchGame
{
    public class CGScore : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
        int totalScore;

        public int TotalScore
        {
            get => totalScore;
            private set
            {
                totalScore = value;
                
                scoreText.text = totalScore.ToString();
            }
        }
        
        private void AddScore(int score)
        {
            totalScore += score;
            scoreText.text = totalScore.ToString();
        }
        
        private void OnDestroy()
        {
            DroppableObject.OnCautch -= AddScore;
        }

        private void Start()
        {
            DroppableObject.OnCautch += AddScore;
        }
    }
}