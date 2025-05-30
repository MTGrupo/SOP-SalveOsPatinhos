﻿using System.Collections;
using Dialogos.Services;
using Duck;
using Interaction;
using MiniGame;
using UnityEngine;

namespace Lixeira
{
    public class TrashBinDialog : InteractableObject, IInteraction
    {
        [SerializeField] private DialogTrashDuck dialog;
        [SerializeField] private DuckBehavior duck;
        [SerializeField] private Collider2D colisor;
        [SerializeField] private GameObject iconeInteracao;
        [SerializeField] private int miniGameID;
        
        public void OnPlayerInteraction()
        {
            GameManager.SaveGameData();
            MiniGameSession.currentMiniGameID = miniGameID;
            dialog.StartDialogo();
        }

        public void EnableDuck()
        {
            duck.gameObject.SetActive(true);
            duck.StartFollowing();
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
                duck.gameObject.SetActive(false);
                AddObject(colisor, this);
                yield break;
            }
            
            yield return new WaitWhile(() => GameManager.IsLoadingGameData);
            
            // O pato já está salvo? Se sim, desativar o ícone de interação e parar processo.
            if (duck.IsRescued)
            {
                iconeInteracao.SetActive(false);
                yield break;
            }
            
            duck.gameObject.SetActive(false);
            
            // O minigame já foi finalizado? se sim, soltar o pato, desativar o ícone de interação e parar processo.
            if (MiniGame.MiniGame.GetState(miniGameID))
            {
                EnableDuck();
                iconeInteracao.SetActive(false);
                yield break;
            }
            
            // Adicionar o colisor na lista de objetos interativos.
            AddObject(colisor, this);
        }
    }
}