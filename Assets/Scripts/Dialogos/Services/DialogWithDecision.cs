using Assets.Scripts.Dialogos.Modal;
using Dialogos.Enum;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogos.Services
{
    public class DialogWithDecision : DialogoBase
    {
        [SerializeField] private Button btnYes;
        [SerializeField] private Button btnNo;
        
        protected override void Start()
        {
            base.Start();
            
            btnYes.gameObject.SetActive(false);
            btnNo.gameObject.SetActive(false);
        }
        
        protected override void ShowDialogo()
        {
            base.ShowDialogo();

            if (dialogoObject.GetDialogoAt(index).TipoDialogoEnum == TipoDialogoEnum.Decisao)
            {
                nextButton.gameObject.SetActive(false);
                btnYes.gameObject.SetActive(true);
                btnNo.gameObject.SetActive(true);
            } else {
                nextButton.gameObject.SetActive(true);
                btnYes.gameObject.SetActive(false);
                btnNo.gameObject.SetActive(false);
            }
        }
        
        protected override void ListenToEvents()
        {
            base.ListenToEvents();
            btnYes.onClick.AddListener(ChooseYes);
            btnNo.onClick.AddListener(ChooseNo);
        }

        void ChooseYes()
        {
            NextDialogo(); 
        }

        void ChooseNo()
        {
            OcultarDialogo();
        }
        
    }
}