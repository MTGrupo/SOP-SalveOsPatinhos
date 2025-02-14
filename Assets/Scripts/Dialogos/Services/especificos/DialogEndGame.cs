using System.Collections;
using Assets.Scripts.Dialogos.Modal;
using Dialogos.Enum;
using Duck;
using UnityEngine;

namespace Dialogos.Services
{
    public class DialogEndGame : DialogoBase
    {
        [SerializeField] private Transform final_point;

        protected override void ShowDialogo()
        {
            base.ShowDialogo();

            if (dialogoObject.GetDialogoAt(index).id == 3)
            {
                dialoguePanel.gameObject.SetActive(false);
                DuckManager.SetEndDestination(final_point);
                StartCoroutine(LoadSceneCredits(2.5f));
                return;
            }
            
            dialoguePanel.gameObject.SetActive(true);   
        }
        
        private IEnumerator LoadSceneCredits(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            GameManager.LoadCredits();
        }
    }
}