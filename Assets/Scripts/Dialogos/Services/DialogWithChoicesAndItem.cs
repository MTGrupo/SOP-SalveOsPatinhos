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
        [SerializeField] private ObjectBase objectBase;
        [SerializeField] private GameObject mensagemItem;
        
        private bool isShowMensagemItem;
        
        protected override void Start()
        {
            base.Start();
            
            btnSim.gameObject.SetActive(false);
            btnNao.gameObject.SetActive(false);
            mensagemItem.SetActive(false);
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
                isShowMensagemItem = true;
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
            btnSim.onClick.AddListener(EscolherSim);
            btnNao.onClick.AddListener(EscolherNao);
        }

        void EscolherSim()
        {
            NextDialogo(); 
        }

        void EscolherNao()
        {
            OcultarDialogo();
        }

        protected override void OnDialogFechado()
        {
            base.OnDialogFechado();

            if (isShowMensagemItem)
            {
                mensagemItem.SetActive(true);

                StartCoroutine(HideMensagemItemAfterDelay(4.5f));
            }
        }

        IEnumerator HideMensagemItemAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            mensagemItem.SetActive(false);
            isShowMensagemItem = false;
        }
    }
}