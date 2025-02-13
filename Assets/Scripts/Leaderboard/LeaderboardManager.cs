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

        public async Task SubmitScore(double score, string leaderboardId)
        {
            if (!AuthenticationManager.Instance.IsAuthenticated) return;
            
            try
            {
                await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
        public async Task<double> GetPlayerScore(string leaderboardId)
        {
            if (!AuthenticationManager.Instance.IsAuthenticated) return 0;
            
            try
            {
                var score = await LeaderboardsService.Instance.GetPlayerScoreAsync(leaderboardId);
                
                return score.Score;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return 0;
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