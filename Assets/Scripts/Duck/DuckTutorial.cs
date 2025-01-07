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
            Debug.Log("DuckTutorial: OnPlayerInteraction");
        }

        private IEnumerator LoadSceneAfterDelay(float delay)
        {
            Debug.Log("DuckTutorial: LoadSceneAfterDelay");
            yield return new WaitForSeconds(delay);
            GameManager.LoadGame();
        }
    }
}