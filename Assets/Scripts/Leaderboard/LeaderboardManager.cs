using System;
using System.Threading.Tasks;
using PlayerAccount;
using Unity.Services.Leaderboards;
using UnityEngine;

namespace Leaderboard
{
    public class LeaderboardManager : MonoBehaviour
    {
        public static LeaderboardManager Instance { get; private set; }

        public async Task SubmitScore(int score)
        {
            if (!AuthenticationManager.Instance.IsAuthenticated) return;
            
            try
            {
                await LeaderboardsService.Instance.AddPlayerScoreAsync("patos_resgatados", score);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void Awake()
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