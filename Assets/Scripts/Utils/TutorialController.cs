using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] RectTransform content;
        [SerializeField] RectTransform[] pages;
        
        int currentPage = 0;
        
        [SerializeField] Button close;
        [SerializeField] Button nextPage;
        [SerializeField] Button previousPage;
        
        public bool IsOPen {
            get => content.gameObject.activeInHierarchy;
            private set 
            {
                content.gameObject.SetActive(value);

                if (value) return;
                
                CatchGame.CatchGame.OnGameStarted?.Invoke();
            }
        }
        
        void UpdateTutorial()
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].gameObject.SetActive(i == currentPage);
            }
            
            previousPage.interactable = currentPage > 0;
            nextPage.interactable = currentPage < pages.Length - 1;
        }
        
        void NextPage()
        {
            if (currentPage >= pages.Length - 1) return;
            
            currentPage++;
            UpdateTutorial();
        }

        void PreviousPage()
        {
            if (currentPage <= 0) return;
            
            currentPage--;
            UpdateTutorial();
        }
        
        void CloseTutorial()
        {
            IsOPen = false;
        }

        private void OnDestroy()
        {
            close.onClick.RemoveListener(CloseTutorial);
            
            if (!nextPage && !previousPage) return;
            
            nextPage.onClick.RemoveListener(NextPage);
            previousPage.onClick.RemoveListener(PreviousPage);
        }

        void Start()
        {
            close.onClick.AddListener(CloseTutorial);

            if (!nextPage && !previousPage) return;
            
            nextPage.onClick.AddListener(NextPage);
            previousPage.onClick.AddListener(PreviousPage);
            
            UpdateTutorial();
        }
        
    }
}