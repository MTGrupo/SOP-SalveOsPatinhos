using Dialogos;
using Dialogos.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        
        public DialogoObject dialogoObject;
        
        protected int index = 0;

        protected virtual void Start()
        {
            ListenToEvents();
            
            zoneCloseDialogue.gameObject.SetActive(false);
        }
        
        public void SetDialogoObject(DialogoObject dialogo)
        {
            dialogoObject = dialogo;
        }
        
        public void StartDialogo()
        {
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
            dialoguePanel.SetActive(true);
           var dialogueActual = dialogoObject.GetDialogoAt(index);
        
           if (dialogueActual.typeSpeaker == TypeSpeaker.SPEAK_PLAYER)
           {
               
               string getNamePlayer = PlayerPrefs.GetString("playerName");
               speaker.text = getNamePlayer;
           }
           else
           {
               speaker.text = dialogueActual.orador;
           }
           
           text.text = dialogueActual.texto;
        }

        protected void OcultarDialogo()
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
            closeButton.onClick.AddListener(OcultarDialogo);
            zoneCloseDialogue.onClick.AddListener(OcultarDialogo);
        }
    }
}