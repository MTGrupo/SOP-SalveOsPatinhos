using Assets.Scripts.Dialogos.Modal;
using Dialogos.Enum;

namespace Dialogos.Services
{
    public class DialogIntro : DialogoBase, IChangeScene
    {
        protected override void ShowDialogo()
        {
            base.ShowDialogo();

            var tipoDialogo = dialogoObject.GetDialogoAt(index).TipoDialogoEnum;
            HandleSceneChange(tipoDialogo);
        }

        public void HandleSceneChange(TipoDialogoEnum tipoDialogoEnum)
        {
            if (tipoDialogoEnum == TipoDialogoEnum.ChangeScene)
            {
                dialoguePanel.gameObject.SetActive(false);
                GameManager.LoadGame();
            }
            else
            {
                dialoguePanel.gameObject.SetActive(true);
            }
        }
    }
}