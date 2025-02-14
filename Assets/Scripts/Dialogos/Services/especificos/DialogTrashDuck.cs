using Assets.Scripts.Dialogos.Modal;
using Dialogos.Enum;

namespace Dialogos.Services
{
    public class DialogTrashDuck : DialogoBase, IChangeScene
    {
        protected override void ShowDialogo()
        {
            base.ShowDialogo();
            nextButton.gameObject.SetActive(true);
            var tipoDialogo = dialogoObject.GetDialogoAt(index).TipoDialogoEnum;
            HandleSceneChange(tipoDialogo);
        }

        public void HandleSceneChange(TipoDialogoEnum tipoDialogoEnum)
        {
            if (tipoDialogoEnum == TipoDialogoEnum.ChangeScene)
            {
                dialoguePanel.SetActive(false);
                GameManager.LoadMiniGame();
            }
            else
            {
                dialoguePanel.SetActive(true);
            } 
        }
    }
}