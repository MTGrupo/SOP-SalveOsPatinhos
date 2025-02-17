using Dialogos;
using Dialogos.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Assets.Scripts.Dialogos.Modal
{
    public abstract class DialogoBase : MonoBehaviour
    {
        [Header("Componentes UI")]
        [SerializeField] protected GameObject dialoguePanel;
        [SerializeField] protected TextMeshProUGUI speaker;
        [SerializeField] protected TextMeshProUGUI text;
        [SerializeField] protected Button nextButton;
        [SerializeField] protected Button closeButton;
        [SerializeField] protected Button zoneCloseDialogue;
        [SerializeField] protected Button zoneFinishDialogue;
        [SerializeField] protected Button speedAnimationDialogue;
        
        public DialogoObject dialogoObject;
        
        protected int index = 0;
        
        private float currentTypingSpeed = 0.05f;  
        private const float fastTypingSpeed = 0.0001f;  
        
        protected virtual void Start()
        {
            ListenToEvents();
            
            if (zoneCloseDialogue)
                zoneCloseDialogue.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
            
            zoneFinishDialogue.gameObject.SetActive(false);
        }
        
        public void SetDialogoObject(DialogoObject dialogo)
        {
            dialogoObject = dialogo;
        }
        
        public void StartDialogo()
        {
            nextButton.gameObject.SetActive(true);
            ShowDialogo();
        }
        
        public void NextDialogo()
        {
            if (index < dialogoObject.dialogos.Count - 1)
            {
                index++;
                ShowDialogo();
            }
            else
            {
                FinishedDialogo();
            }
        }

        public void NextDialogoNotShow()
        {
            index++;
        }
        
        protected virtual void ShowDialogo()
        {
            nextButton.interactable = false;
            
            if (closeButton)
                closeButton.interactable = false;
            
            if (zoneCloseDialogue)
                zoneCloseDialogue.interactable = false;
    
            dialoguePanel.SetActive(true);
            var dialogueActual = dialogoObject.GetDialogoAt(index);
            
            if (speedAnimationDialogue)
                speedAnimationDialogue.gameObject.SetActive(true);
            
            if (dialogueActual.typeSpeaker == TypeSpeaker.SPEAK_PLAYER)
            {
                string getNamePlayer = PlayerPrefs.GetString("playerName");
                
                AnimatorText.AnimationSettings(align: Alignment.Left, speed: currentTypingSpeed);
                StartCoroutine(AnimatorText.StartAnimatorText(speaker, getNamePlayer));
            }
            else
            {
                AnimatorText.AnimationSettings(align: Alignment.Right, speed: currentTypingSpeed);
                StartCoroutine(AnimatorText.StartAnimatorText(speaker, dialogueActual.orador));
            }
            
            AnimatorText.AnimationSettings(align: Alignment.Left, step: 1, speed: currentTypingSpeed);
            StartCoroutine(AnimatorText.StartAnimatorText(text, dialogueActual.texto, EnableButtonsAfterAnimation));
        }

        protected virtual void EnableButtonsAfterAnimation()
        {
            nextButton.interactable = true;
            
            if (closeButton)
            {
                closeButton.interactable = true;
            }

            if (zoneCloseDialogue)
            {
                zoneCloseDialogue.interactable = true;
            }
            
            if (speedAnimationDialogue)
                speedAnimationDialogue.gameObject.SetActive(false);
        }
        
        protected virtual void OcultarDialogo()
        {
            dialoguePanel.SetActive(false);
            OnDialogFechado();
        }

        protected virtual void OnDialogFechado() {}
        
        protected virtual void FinishedDialogo()
        {
            dialoguePanel.SetActive(false);
            index = 0;
        }
        
        protected virtual void ListenToEvents()
        {
            nextButton.onClick.AddListener(NextDialogo);
            if (closeButton)
                closeButton.onClick.AddListener(OcultarDialogo);
            
            if (zoneCloseDialogue)
                zoneCloseDialogue.onClick.AddListener(OcultarDialogo);
            
            if (zoneFinishDialogue)
                zoneFinishDialogue.onClick.AddListener(FinishedDialogo);
            
            if (speedAnimationDialogue)
                speedAnimationDialogue.onClick.AddListener(ToggleSpeedAnimation);
        }
        
        private void ToggleSpeedAnimation()
        {
            if (currentTypingSpeed == fastTypingSpeed)
            {
                currentTypingSpeed = 0.05f;  
                Debug.Log("Velocidade normal");
            }
            else
            {
                currentTypingSpeed = fastTypingSpeed; 
                Debug.Log("Velocidade rápida");
            }
            
            AnimatorText.AnimationSettings(speed: currentTypingSpeed);
        }

    }
}
