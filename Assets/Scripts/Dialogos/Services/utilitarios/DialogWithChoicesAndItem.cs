using System.Collections;
using Dialogos.Enum;
using Dialogos.ObjectsOfDialogos;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogos.Services
{
    public class DialogWithChoicesAndItem : DialogDuckBase
    {
        [SerializeField] private Button btnSim;
        [SerializeField] private Button btnNao;
        [SerializeField] protected ObjectBase objectBase;
        [SerializeField] private GameObject mensagemItem;
        [SerializeField] private Animator animator; 
        
        private bool isShowMensagemItem;
        
        protected override void Start()
        {
            base.Start();
            
            btnSim.gameObject.SetActive(false);
            btnNao.gameObject.SetActive(false);
            mensagemItem.SetActive(true);
        }

        protected override void ShowDialogo()
        {
            base.ShowDialogo();

            if (dialogoObject.GetDialogoAt(index).TipoDialogoEnum == TipoDialogoEnum.Decisao)
            {
                btnSim.gameObject.SetActive(false);
                btnNao.gameObject.SetActive(false);
                nextButton.gameObject.SetActive(false);
            }
            else
            {
                btnSim.gameObject.SetActive(false);
                btnNao.gameObject.SetActive(false);
            }
            
            if (dialogoObject.GetDialogoAt(index).TipoDialogoEnum == TipoDialogoEnum.Buscando_Itens)
            {
                objectBase.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(false);
            }
        }

        protected override void EnableButtonsAfterAnimation()
        {
            base.EnableButtonsAfterAnimation();

            if (dialogoObject.GetDialogoAt(index).TipoDialogoEnum == TipoDialogoEnum.Decisao)
            {
                btnSim.gameObject.SetActive(true);
                btnNao.gameObject.SetActive(true);
            }
        }
        
        protected override void ListenToEvents()
        {
            base.ListenToEvents();
            btnSim.onClick.AddListener(ChooseYes);
            btnNao.onClick.AddListener(ChooseNo);
        }

        void ChooseYes()
        {
            NextDialogo(); 
        }

        void ChooseNo()
        {
            OcultarDialogo();
        }

        protected override void OnDialogFechado()
        {
            base.OnDialogFechado();
            
            mensagemItem.SetActive(true);
            animator.SetTrigger("open");

            StartCoroutine(HideMensagemItemAfterDelay(3f));
        }

        IEnumerator HideMensagemItemAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            animator.SetTrigger("open");
        }
    }
}