using System.Collections;
using System.Collections.Generic;
using Dialogos.Services.especificos;
using Duck;
using Interaction;
using MiniGame;
using UnityEngine;

namespace Coqueiro
{
    public class CocknutTreeWithDuck : InteractableObject, IInteraction
    {
        [SerializeField] private DialogDuckOnTree dialog;
        [SerializeField] private List<DuckBehavior> ducks;
        [SerializeField] private Collider2D colisor;
        [SerializeField] private GameObject iconeInteracao;
        [SerializeField] private GameObject duckOnTree;
        [SerializeField] private int miniGameID;
        
        public void OnPlayerInteraction()
        {
            MiniGameSession.currentMiniGameID = miniGameID;
            GameManager.SaveGameData();
            dialog.StartDialogo();
        }

        public void EnableDuck(int index)
        {
            ducks[index].gameObject.SetActive(true);
            ducks[index].StartFollowing();
            GameManager.SaveGameData();
        }
        
        void OnDestroy()
        {
            RemoveObject(colisor);
        }
        
        private IEnumerator Start()
        {
            // É um jogo novo? Se sim, parar processo.
            if(!GameManager.IsLoadingGameData)
            {
                foreach (var duck in ducks)
                {
                    duck.gameObject.SetActive(false);
                }
                AddObject(colisor, this);
                yield break;
            }
            
            yield return new WaitWhile(() => GameManager.IsLoadingGameData);
            
            // Algum pato já foi resgatado? Se sim, desativar o ícone de interação e parar processo.
            var isDuckRescued = false;
            foreach (var duck in ducks)
            {
                if (!duck.IsRescued)
                {
                    duck.gameObject.SetActive(false);
                    continue;
                }

                isDuckRescued = true;
            }

            if (isDuckRescued)
            {
                iconeInteracao.SetActive(false);
                duckOnTree.SetActive(false);
                yield break;
            }
            
            // O minigame já foi finalizado? se sim, soltar a quantidade de patos resgatados, desativar o ícone de interação e parar processo.
            var catchGameResults = CatchGame.CatchGame.GetResult(miniGameID);
            if (catchGameResults.isFinished)
            {
                for (int i = 0; i < catchGameResults.ducksRecued; i++)
                {
                    EnableDuck(i);
                }
                iconeInteracao.SetActive(false);
                duckOnTree.SetActive(false);
                yield break;
            }
            
            // Adicionar o colisor na lista de objetos interativos.
            AddObject(colisor, this);
        }
    }
}