using DefaultNamespace.Inventory;
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

                if (SlotManager.GetSlotDataByName("coco")?.amount >= 3)
                {
                    dialogoObject.GetDialogoAt(index).texto = "Tome aqui pato, seus 3 cocos.";
                    base.ShowDialogo();
                    nextButton.gameObject.SetActive(true);
                    zoneCloseDialogue.gameObject.SetActive(false);
                    DeliverCoco();
                    return;
                }

                dialogoObject.GetDialogoAt(index).texto = "Claro Pato, irei pegar os cocos no coqueiro.";
                base.ShowDialogo();
                nextButton.gameObject.SetActive(false);
                isDialogoCoco = true;
                return;
            }
            
            zoneCloseDialogue.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
        }

        protected override void FinishedDialogo()
        {
            base.FinishedDialogo();
            DeliverCoco();
        }

        private void DeliverCoco()
        {
            if (SlotManager.RemoveItemFromInventory("coco", 3))
            {
                SlotManager.LoadSlotData();
                isDialogoCoco = false;
            }
            else
            {
                Debug.Log("Não há cocos sufucientes para entregar");
            }
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