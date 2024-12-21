using Dialogos.Enum;
using UnityEngine;

namespace Dialogos.Services
{
    public class DialogDuckAguaDeCoco : DialogDuckBase
    {
        public static DialogDuckAguaDeCoco Instance { get; private set; }
        
        public bool isDialogoCoco = false;
        public void ShowMensagemCoco()
        {
            NextDialogoNotShow();
        }
        protected override void ShowDialogo()
        {
            base.ShowDialogo();
            
            if (dialogoObject.GetDialogoAt(index).TipoDialogoEnum == TipoDialogoEnum.Buscando_Itens)
            {
                zonaDeFechar.gameObject.SetActive(true);
                botaoProximo.gameObject.SetActive(false);
                Debug.Log("ShowDialogo");
                isDialogoCoco = true;
                return;
            }
            
            zonaDeFechar.gameObject.SetActive(false);
            botaoProximo.gameObject.SetActive(true);
        }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this; 
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}