using System;
using UnityEngine;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine.UI;

namespace Menus
{
    public class LeaderboardMenu : MenuBehaviour
    {
        [SerializeField] Transform content;
        [SerializeField] GameObject leaderboardRowPrefab;
        [SerializeField] GameObject playerRow;
        [SerializeField] Transform scrollViewContent;
        [SerializeField] Button backButton;
        [SerializeField] Toggle toggleButton;
        [SerializeField] TextMeshProUGUI RankingText;

        protected override bool IsOpen
        {
            get => content.gameObject.activeInHierarchy;
            set
            {
                content.gameObject.SetActive(value);

                if (!value) return;
                
                LoadLeaderboard(toggleButton.isOn);
            } 
        }
        
        void Back()
        {
            switch (GameManager.CurrentState)
            {
                case GameState.Menu:
                    ShowMainMenu();
                    return;
                default:
                    Debug.Log("Provavelmente falta configurar algo.", this);
                    break;
            }
        }
        
        void Awake()
        {
            LeaderboardMenu = this;
            Close();
			
            backButton.onClick.AddListener(Back);
            toggleButton.onValueChanged.AddListener(LoadLeaderboard);
        }

        private async void LoadLeaderboard(bool isDuckRanking)
        {
            try
            {
                ClearLeaderboard();

                LeaderboardScoresPage scores;
                
                if (isDuckRanking)
                {
                    scores = await LeaderboardsService.Instance.GetScoresAsync("patos_resgatados");
                    RankingText.text = "Patos Resgatados";
                }
                else
                {
                    scores = await LeaderboardsService.Instance.GetScoresAsync("menor_tempo");
                    RankingText.text = "Minutos para Zerar";
                }
                
                string currentPlayerId = AuthenticationService.Instance.PlayerId;

                var position = 1;
                
                foreach (var entry in scores.Results)
                {
                    var row = Instantiate(leaderboardRowPrefab, scrollViewContent);
                    
                    if (entry.PlayerId == currentPlayerId)
                    {
                        UpdateLeaderboardRow(row, position, entry.PlayerName+ " (Você)", entry.Score, isDuckRanking);
                        UpdateLeaderboardRow(playerRow, position, null, entry.Score, isDuckRanking);
                    }
                    else
                    {
                        UpdateLeaderboardRow(row, position, entry.PlayerName, entry.Score, isDuckRanking);
                    }
                    
                    position++;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        
        private void UpdateLeaderboardRow(GameObject row, int position, string name, double score, bool isDuckRanking)
        {
            var columns = row.GetComponentsInChildren<TMP_Text>();

            
            
            TMP_Text positionText = columns[0];
            TMP_Text nameText = columns[1];
            TMP_Text scoreText = columns[2];

            positionText.text = position.ToString();

            if (isDuckRanking)
            {
                scoreText.text = score.ToString();
            }
            else
            {
                int scoreMinutes = Mathf.FloorToInt((float)(score / 60));
                int scoreSeconds = Mathf.FloorToInt((float)(score % 60));
                scoreText.text = $"{scoreMinutes:00}:{scoreSeconds:00}";
            }

            if (name == null) return;
            nameText.text = name;
        }
        
        void ClearLeaderboard()
        {
            if (scrollViewContent.childCount == 0) return;
            
            foreach (Transform child in scrollViewContent)
            {
                Destroy(child.gameObject);
            }
            
            var playerInfo = playerRow.GetComponentsInChildren<TextMeshProUGUI>();

            playerInfo[0].text = "-";
            playerInfo[2].text = "-";
        }
    }
}