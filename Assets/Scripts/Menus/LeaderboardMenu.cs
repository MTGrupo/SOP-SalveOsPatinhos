using System;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
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

        protected override bool IsOpen
        {
            get => content.gameObject.activeInHierarchy;
            set
            {
                content.gameObject.SetActive(value);

                if (!value) return;
                
                ClearLeaderboard();
                
                _ = LoadLeaderboard(); 
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
        }

        private async Task LoadLeaderboard()
        {
            try
            {
                var scores = await LeaderboardsService.Instance.GetScoresAsync("patos_resgatados");
                string currentPlayerId = AuthenticationService.Instance.PlayerId;

                var position = 1;
                
                foreach (var entry in scores.Results)
                {
                    var row = Instantiate(leaderboardRowPrefab, scrollViewContent);
                    
                    if (entry.PlayerId == currentPlayerId)
                    {
                        UpdateLeaderboardRow(row, position, entry.PlayerName+ " (Você)", entry.Score);
                        UpdateLeaderboardRow(playerRow, position, "Você", entry.Score);
                    }
                    else
                    {
                        UpdateLeaderboardRow(row, position, entry.PlayerName, entry.Score);
                    }
                    
                    position++;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        
        private void UpdateLeaderboardRow(GameObject row, int position, string name, double score)
        {
            var columns = row.GetComponentsInChildren<TMP_Text>();

            TMP_Text positionText = columns[0];
            TMP_Text nameText = columns[1];
            TMP_Text scoreText = columns[2];

            positionText.text = position.ToString();
            nameText.text = name;
            scoreText.text = score.ToString();
        }
        
        void ClearLeaderboard()
        {
            if (scrollViewContent.childCount == 0) return;
            
            foreach (Transform child in scrollViewContent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}