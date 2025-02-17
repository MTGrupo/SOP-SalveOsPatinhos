using Assets.Scripts.Dialogos.Modal;
using Dialogos.Enum;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        protected override void OcultarDialogo()
        {
            base.OcultarDialogo();
            GameManager.LoadGame();
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