using System.Collections;
using Dialogos.Enum;
using Dialogos.ObjectsOfDialogos;
using UnityEngine;

namespace Dialogos.Services
{
    public class DialogWithItem : DialogDuckBase
    {
        [SerializeField] private ObjectBase objectBase;
        [SerializeField] private GameObject mensagemItem;
        
        private bool isShowMensagemItem;
        
        protected override void Start()
        {
            base.Start();
            mensagemItem.SetActive(false);
        }

        protected override void ShowDialogo()
        {
            base.ShowDialogo();
            
            if (dialogoObject.GetDialogoAt(index).TipoDialogoEnum == TipoDialogoEnum.Buscando_Itens)
            {
                objectBase.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(false);
                isShowMensagemItem = true;
            }
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