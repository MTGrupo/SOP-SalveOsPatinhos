using Assets.Scripts.Dialogos.Modal;
using Duck;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameEnd
{
    public class GameEnd : MonoBehaviour
    {
        [SerializeField] private DialogoBase endDialogText;
        [SerializeField] private GameObject endQuestText;
        [SerializeField] private Collider2D initialPoint;
        [SerializeField] private AudioSource praiaSong;
        [SerializeField] private Collider2D endPoint;
        
        [SerializeField] private Button openPainelButton;
        [SerializeField] private GameObject gameEndPainel;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button cancelButton;
        
        [SerializeField] private Movement movement;

        void CheckRescuedDucks()
        {
            if (DuckManager.RescuedCount == 19) return;
            
            if(openPainelButton.gameObject.activeSelf) return;
            
            ShowGameEndPainel();
        }

        void StartGameEndQuest()
        {
            HideGameEndPainel(true);
            endQuestText.gameObject.SetActive(true);
            initialPoint.enabled = true;
        }
        
        void ShowGameEndPainel()
        {
            movement.Disable();
            gameEndPainel.SetActive(true);

            if (!openPainelButton.gameObject.activeSelf) return;
            
            openPainelButton.gameObject.SetActive(false);
        }
        
        private void HideGameEndPainel()
        {
            movement.Enable();
            gameEndPainel.SetActive(false);
            openPainelButton.gameObject.SetActive(true);
        }
        
        void HideGameEndPainel(bool isStartingQuest)
        {
            movement.Enable();
            gameEndPainel.SetActive(false);
            
            if (isStartingQuest) return;
            openPainelButton.gameObject.SetActive(true);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
        
            praiaSong.Stop();
            initialPoint.enabled = false;
            endPoint.enabled = true;
            endQuestText.SetActive(false);
            endDialogText.StartDialogo();
            AudioController.PlaySong();
        }
        
        void OnDestroy()
        {
            DuckBehavior.OnDuckRescued -= CheckRescuedDucks;
            
            openPainelButton.onClick.RemoveListener(ShowGameEndPainel);
            confirmButton.onClick.RemoveListener(StartGameEndQuest);
            cancelButton.onClick.RemoveListener(HideGameEndPainel);
        }
        
        void Awake()
        {
            DuckBehavior.OnDuckRescued += CheckRescuedDucks;
            
            openPainelButton.onClick.AddListener(ShowGameEndPainel);
            confirmButton.onClick.AddListener(StartGameEndQuest);
            cancelButton.onClick.AddListener(HideGameEndPainel);
        }
    }
}
