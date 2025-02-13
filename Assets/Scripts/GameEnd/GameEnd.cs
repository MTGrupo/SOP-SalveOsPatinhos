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
        
        [SerializeField] private Button openGameEndPainel;
        [SerializeField] private GameObject gameEndPainel;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button cancelButton;

        void Awake()
        {
            DuckBehavior.OnDuckRescued += CheckRescuedDucks;
            
            openGameEndPainel.onClick.AddListener(() => openGameEndPainel.gameObject.SetActive(false));
            openGameEndPainel.onClick.AddListener(() => gameEndPainel.SetActive(true));
            
            confirmButton.onClick.AddListener(() => gameEndPainel.SetActive(false));
            confirmButton.onClick.AddListener(StartGameEndQuest);
            
            cancelButton.onClick.AddListener(() => gameEndPainel.SetActive(false));
            cancelButton.onClick.AddListener(() => openGameEndPainel.gameObject.SetActive(true));
        }
        
        void CheckRescuedDucks()
        {
            if (DuckManager.RescuedCount < 19) return;
            
            if(openGameEndPainel.gameObject.activeSelf) return;
            
            gameEndPainel.SetActive(true);
        }

        void StartGameEndQuest()
        {
            endQuestText.gameObject.SetActive(true);
            initialPoint.enabled = true;
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
            
            openGameEndPainel.onClick.RemoveAllListeners();
            
            confirmButton.onClick.RemoveAllListeners();
            cancelButton.onClick.RemoveAllListeners();
        }
    }
}
