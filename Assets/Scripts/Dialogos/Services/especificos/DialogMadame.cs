using DefaultNamespace.Inventory;
using Dialogos.Enum;

namespace Dialogos.Services
{
    public class DialogMadame : DialogWithChoicesAndItem
    {
        protected override void ShowDialogo()
        {
            base.ShowDialogo();
            if (dialogoObject.GetDialogoAt(index).id == 2)
            {
                if (SlotManager.GetSlotDataByName("chapeu")?.amount >= 1)
                {
                    dialogoObject.GetDialogoAt(index).texto = "Eu tenho um chapéu aqui";
                    dialogoObject.GetDialogoAt(index).TipoDialogoEnum = TipoDialogoEnum.Normal;
                    return; 
                }
                
                dialogoObject.GetDialogoAt(index).texto = "Vamos procurar pelo seu chapéu.";
                dialogoObject.GetDialogoAt(index).TipoDialogoEnum = TipoDialogoEnum.Buscando_Itens;
                base.ShowDialogo();
                return; 
            }
            
            if (dialogoObject.GetDialogoAt(index).id == 3)
            {
                if (SlotManager.GetSlotDataByName("chapeu")?.amount >= 1)
                {
                    dialogoObject.GetDialogoAt(index).texto = "Obrigado pelo chapéu!";
                    dialogoObject.GetDialogoAt(index).TipoDialogoEnum = TipoDialogoEnum.FinisheDialog;
                    objectBase.gameObject.SetActive(false);
                    base.ShowDialogo();
                    return; 
                }
            }
            
            base.ShowDialogo();
        }

        protected override void FinishedDialogo()
        {
            base.FinishedDialogo();
            DropItem.Drop("chapeu", 1);
            SlotManager.LoadSlotData();
        }
    }
}