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
            
            if (zonaDeFechar == null) return;
            
            if (dialogoObject.GetDialogoAt(index).TipoDialogoEnum == TipoDialogoEnum.LastSpeak || 
                dialogoObject.GetDialogoAt(index).TipoDialogoEnum == TipoDialogoEnum.Buscando_Itens)
            {
                botaoProximo.gameObject.SetActive(false);
                zonaDeFechar.gameObject.SetActive(true);
                return;
            }
            
            botaoProximo.gameObject.SetActive(true);
            zonaDeFechar.gameObject.SetActive(false);
        }
    }
}