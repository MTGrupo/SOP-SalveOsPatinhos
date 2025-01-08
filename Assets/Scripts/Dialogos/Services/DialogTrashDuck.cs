using System.Collections;
using Duck;
using UnityEngine;

namespace Dialogos.Services
{
    public class DialogTrashDuck : DialogWithDecision
    {
        [SerializeField] private DuckBehavior duck;

        protected override void ShowDialogo()
        {
            
            base.ShowDialogo();
            
            if (dialogoObject.GetDialogoAt(index).id == 3)
            {
                StartCoroutine(AdvanceScene(3f));
                botaoProximo.gameObject.SetActive(false);
                botaoFechar.gameObject.SetActive(false);
            }
            else
            {
                botaoProximo.gameObject.SetActive(true);
                botaoFechar.gameObject.SetActive(true);
            }
        }

        private IEnumerator AdvanceScene(float delay)
        {
            yield return new WaitForSeconds(delay);
            GameManager.LoadMiniGame();
        }
    }
}