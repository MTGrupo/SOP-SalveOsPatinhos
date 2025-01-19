using Assets.Scripts.Dialogos.Modal;
using Dialogos.Enum;
using Duck;
using UnityEngine;

namespace Dialogos.Services
{
    public class DialogDuckBase : DialogoBase
    {
        [SerializeField] protected DuckDialog duck;
        protected override void FinishedDialogo()
        {
            base.FinishedDialogo();

            if (duck)
            {
                duck.StartFollowing();
                GameManager.SaveGameData();  
            }
            
        }

        protected override void ShowDialogo()
        {
            base.ShowDialogo();
            
            if (zoneCloseDialogue == null) return;
            
            if (dialogoObject.GetDialogoAt(index).TipoDialogoEnum == TipoDialogoEnum.LastSpeak || 
                dialogoObject.GetDialogoAt(index).TipoDialogoEnum == TipoDialogoEnum.Buscando_Itens)
            {
                nextButton.gameObject.SetActive(false);
                zoneCloseDialogue.gameObject.SetActive(true);
                return;
            }
            
            nextButton.gameObject.SetActive(true);
            zoneCloseDialogue.gameObject.SetActive(false);
        }
    }
}