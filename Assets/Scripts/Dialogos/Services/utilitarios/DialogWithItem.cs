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
        [SerializeField] private Animator mensagemAnimator;
        
        private bool isShowMensagemItem;

        protected override void ShowDialogo()
        {
            base.ShowDialogo();
            
            if (dialogoObject.GetDialogoAt(index).TipoDialogoEnum == TipoDialogoEnum.Buscando_Itens)
            {
                objectBase.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(false);
                mensagemItem.SetActive(true);
                mensagemAnimator.SetTrigger("open");
            }
        }

        protected override void OnDialogFechado()
        {
            base.OnDialogFechado();
            
            if (isShowMensagemItem)
            {
                StartCoroutine(HideMensagemItemAfterDelay(4.5f));
            }
        }

        IEnumerator HideMensagemItemAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            mensagemAnimator.SetTrigger("open");
        }
    }
}