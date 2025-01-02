using System;
using DefaultNamespace;
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
        [SerializeField] protected GameObject dialogoPainel;
        [SerializeField] private TextMeshProUGUI orador;
        [SerializeField] private TextMeshProUGUI texto;
        [SerializeField] protected Button botaoProximo;
        [SerializeField] protected Button botaoFechar;
        [SerializeField] protected Button zonaDeFechar;

        public DialogoObject dialogoObject;
        protected int index = 0;
        
        protected virtual void Start()
        {
            ListenToEvents();
            
            zonaDeFechar.gameObject.SetActive(false);
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
           dialogoPainel.SetActive(true);
           var dialogoAtual = dialogoObject.GetDialogoAt(index);
        
           if (dialogoAtual.typeSpeaker == TypeSpeaker.SPEAK_PLAYER)
           {
               string namePlayer = PlayerNameGenarator.GetPlayerName(PlayerManager.Instance.PlayerName);
               orador.text = namePlayer;
           }
           else
           {
               orador.text = dialogoAtual.orador;
           }
           
           texto.text = dialogoAtual.texto;
        }

        protected void OcultarDialogo()
        {
            dialogoPainel.SetActive(false);
            OnDialogFechado();
        }

        protected virtual void OnDialogFechado() {}
        
        protected virtual void FinishedDialogo()
        {
            dialogoPainel.SetActive(false);
            index = 0;
        }
        
        protected virtual void ListenToEvents()
        {
            botaoProximo.onClick.AddListener(NextDialogo);
            botaoFechar.onClick.AddListener(OcultarDialogo);
            zonaDeFechar.onClick.AddListener(OcultarDialogo);
        }
    }
}