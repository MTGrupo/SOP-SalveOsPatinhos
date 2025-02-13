using UnityEngine;
using UnityEngine.UI;
namespace Leaderboard
{
    public class RankingToggleGraphic : MonoBehaviour
    {
        [SerializeField] string _toogleParameter;
        [SerializeField] Animator animator;
        [SerializeField] Toggle toggleButton;
        
        public bool IsDuckRanking
        {
            get => animator.GetBool(_toogleParameter);
            private set => animator.SetBool(_toogleParameter, value);
        }
        
        void ChangeGraphic(bool isDuckRanking)
        {
            IsDuckRanking = isDuckRanking;
        }

        private void OnDestroy()
        {
            toggleButton.onValueChanged.RemoveListener(ChangeGraphic);
        }

        void Awake()
        {
            toggleButton.onValueChanged.AddListener(ChangeGraphic);
        }
    }
}