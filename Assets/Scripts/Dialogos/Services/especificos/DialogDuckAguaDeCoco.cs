using DefaultNamespace.Inventory;
using Dialogos.Enum;
using Dialogos.Services.utilitarios.Interfaces;

namespace Dialogos.Services
{
    public class DialogDuckAguaDeCoco : DialogDuckBase
    { 
        private IDialogItemChecker itemChecker = new DialogItemChecker();
        public static DialogDuckAguaDeCoco Instance { get; private set; }
        
        public bool isDialogoCoco = false;
        
        public void ShowMensagemCoco()
        {
            NextDialogoNotShow();
        }

        private void Start()
        {
            itemChecker.AddCondition(dialogId: 2, itemName: "coco", requiredAmount: 3, 
                trueAction: () =>
                {
                    dialogoObject.GetDialogoAt(index).texto = "Tome aqui pato, seus 3 cocos.";
                    nextButton.gameObject.SetActive(true);
                    zoneCloseDialogue.gameObject.SetActive(false);
                    dialogoObject.GetDialogoAt(index).TipoDialogoEnum = TipoDialogoEnum.FinisheDialog;
                    base.ShowDialogo();
                },
                falseAction: () =>
                {
                    dialogoObject.GetDialogoAt(index).texto = "Claro Pato, irei pegar os cocos no coqueiro.";
                    nextButton.gameObject.SetActive(false);
                    isDialogoCoco = true;
                    dialogoObject.GetDialogoAt(index).TipoDialogoEnum = TipoDialogoEnum.Buscando_Itens;
                    base.ShowDialogo();
                });
            base.Start();
        }
        protected override void ShowDialogo()
        {
            if (itemChecker.CheckAndExecute(dialogoObject.GetDialogoAt(index).id)) return;
            
            base.ShowDialogo();
        }

        protected override void FinishedDialogo()
        {
            base.FinishedDialogo();
            DeliverCoco();
        }

        private void DeliverCoco()
        {
            if (DropItem.Drop("coco", 3))
            {
                isDialogoCoco = false;
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