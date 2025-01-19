using System.Collections;
using Dialogos.Services;
using UnityEngine;

namespace Duck
{
    public class DuckTutorial : DuckBehavior
    {
        [SerializeField] private DialogTutorial dialogTutorial;
        public override void OnPlayerInteraction()
        {
            base.OnPlayerInteraction();
            dialogTutorial.NextDialogo();
            StartCoroutine(LoadSceneAfterDelay(8));
        }

        private IEnumerator LoadSceneAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            GameManager.LoadGame();
        }
    }
}