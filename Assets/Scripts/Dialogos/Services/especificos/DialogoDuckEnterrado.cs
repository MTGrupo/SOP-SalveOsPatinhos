using Dialogos.Enum;
using Dialogos.Services.utilitarios.Interfaces;

namespace Dialogos.Services.especificos
{
    public class DialogoDuckEnterrado : DialogWithChoicesAndItem
    {
        private IDialogItemChecker itemChecker = new DialogItemChecker();

        private void Start()
        {
            itemChecker.AddCondition(dialogId:3, itemName: "pa", requiredAmount: 1, () =>
            {
                dialogoObject.GetDialogoAt(index).texto = "Não se preocupe, eu tenho uma pá aqui para te ajudar";
                dialogoObject.GetDialogoAt(index).TipoDialogoEnum = TipoDialogoEnum.FinisheDialog;
                base.ShowDialogo();
            });
            
            base.Start();
        }

        protected override void ShowDialogo()
        {
            if (itemChecker.CheckAndExecute(dialogoObject.GetDialogoAt(index).id)) return;

            if (dialogoObject.GetDialogoAt(index).id == 3)
            {
                dialogoObject.GetDialogoAt(index).texto = "Claro, vou te ajudar! Não se preocupe.";
                dialogoObject.GetDialogoAt(index).TipoDialogoEnum = TipoDialogoEnum.Normal;
                base.ShowDialogo();
            }
            base.ShowDialogo();
        }
    }
}