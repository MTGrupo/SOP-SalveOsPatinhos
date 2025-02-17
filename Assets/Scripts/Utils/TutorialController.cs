using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Utils
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] RectTransform content;
        [SerializeField] RectTransform[] pages;
        
        int currentPage = 0;
        
        [SerializeField] Button closeButton;
        [SerializeField] Button nextPage;
        [SerializeField] Button previousPage;
        [SerializeField] Button continueButton;

        public static event Action OnTutorialClosed;
        
        public bool IsOPen {
            get => content.gameObject.activeInHierarchy;
            private set 
            {
                content.gameObject.SetActive(value);

                if (value) return;
                
                OnTutorialClosed?.Invoke();
            }
        }
        
        void UpdateTutorial()
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].gameObject.SetActive(i == currentPage);
            }
            
            previousPage.gameObject.SetActive(currentPage > 0);
            nextPage.gameObject.SetActive(currentPage < pages.Length - 1);
            continueButton.gameObject.SetActive(currentPage == pages.Length - 1);
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
            closeButton.onClick.RemoveListener(CloseTutorial);
            continueButton.onClick.RemoveListener(CloseTutorial);
            
            if (!nextPage && !previousPage) return;
            
            nextPage.onClick.RemoveListener(NextPage);
            previousPage.onClick.RemoveListener(PreviousPage);
        }

        void Start()
        {
            closeButton.onClick.AddListener(CloseTutorial);
            continueButton.onClick.AddListener(CloseTutorial);

            if (!nextPage && !previousPage) return;
            
            nextPage.onClick.AddListener(NextPage);
            previousPage.onClick.AddListener(PreviousPage);
            
            UpdateTutorial();
        }
        
    }
}