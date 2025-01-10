using Assets.Scripts.Dialogos.Modal;
using Dialogos.Enum;

namespace Dialogos.Services
{
    public class DialogIntro : DialogoBase
    {
        protected override void ShowDialogo()
        {
            base.ShowDialogo();
            
            if (dialogoObject.GetDialogoAt(index).TipoDialogoEnum == TipoDialogoEnum.ChangeScene)
            {
                dialogoPainel.gameObject.SetActive(false);
                GameManager.LoadTutorial();
                return;
            }
            
            dialogoPainel.gameObject.SetActive(true);
        }
    }
}