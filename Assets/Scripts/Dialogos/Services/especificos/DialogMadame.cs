using DefaultNamespace.Inventory;
using Dialogos.Enum;
using Dialogos.Services.utilitarios.Interfaces;

namespace Dialogos.Services
{
    public class DialogMadame : DialogWithChoicesAndItem
    {
        private IDialogItemChecker itemChecker = new DialogItemChecker();

        private void Start()
        {
            itemChecker.AddCondition(dialogId:2, itemName: "chapeu", requiredAmount: 1, () =>
            {
                dialogoObject.GetDialogoAt(index).texto = "Eu tenho um chapéu aqui";
                dialogoObject.GetDialogoAt(index).TipoDialogoEnum = TipoDialogoEnum.Normal;
            });
            
            itemChecker.AddCondition(dialogId:3, itemName:"chapeu", requiredAmount:1, () =>
            {
                dialogoObject.GetDialogoAt(index).texto = "Obrigado pelo chapéu!";
                dialogoObject.GetDialogoAt(index).TipoDialogoEnum = TipoDialogoEnum.FinisheDialog;
                objectBase.gameObject.SetActive(false);
            });
            
            base.Start();
        }
        
        protected override void ShowDialogo()
        {
            base.ShowDialogo();
            if (itemChecker.CheckAndExecute(dialogoObject.GetDialogoAt(index).id)) return;
            base.ShowDialogo();
            
            // Caso não tenha o item, segue o diálogo padrão
            if (dialogoObject.GetDialogoAt(index).id == 2)
            {
                dialogoObject.GetDialogoAt(index).texto = "Vamos procurar pelo seu chapéu.";
                dialogoObject.GetDialogoAt(index).TipoDialogoEnum = TipoDialogoEnum.Buscando_Itens;
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