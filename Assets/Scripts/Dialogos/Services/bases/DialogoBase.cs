using Dialogos;
using Dialogos.Enum;
using Player;
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
        
        
        public DialogoObject dialogoObject;
        private InputsBehaviour inputsBehaviour;
        
        protected int index = 0;
        
        protected virtual void Start()
        {
            inputsBehaviour = FindObjectOfType<InputsBehaviour>();
            
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
            inputsBehaviour.gameObject.SetActive(false);
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
            inputsBehaviour.gameObject.SetActive(false);
            nextButton.interactable = false;
            
            if (closeButton)
                closeButton.interactable = false;
            
            if (zoneCloseDialogue)
                zoneCloseDialogue.interactable = false;
    
            dialoguePanel.SetActive(true);
            var dialogueActual = dialogoObject.GetDialogoAt(index);

            if (dialogueActual.typeSpeaker == TypeSpeaker.SPEAK_PLAYER)
            {
                string getNamePlayer = PlayerPrefs.GetString("playerName");
                
                AnimatorText.AnimationSettings(align: Alignment.Left,useClicked: false);
                StartCoroutine(AnimatorText.StartAnimatorText(speaker, getNamePlayer));
            }
            else
            {
                AnimatorText.AnimationSettings(align: Alignment.Right, useClicked: false);
                StartCoroutine(AnimatorText.StartAnimatorText(speaker, dialogueActual.orador));
            }
            
            AnimatorText.AnimationSettings(align: Alignment.Left, step: 1);
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
        }

        
        protected void OcultarDialogo()
        {
            inputsBehaviour.gameObject.SetActive(true);
            dialoguePanel.SetActive(false);
            OnDialogFechado();
        }

        protected virtual void OnDialogFechado() {}
        
        protected virtual void FinishedDialogo()
        {
            inputsBehaviour.gameObject.SetActive(true);
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
        }
        
    }
}