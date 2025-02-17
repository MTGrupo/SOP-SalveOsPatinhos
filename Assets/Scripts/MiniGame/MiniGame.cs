using System;
using System.Collections.Generic;
using Dialog;
using UnityEngine;
using UnityEngine.Events;
using Dialogo = Dialogos.Dialogo;
using Random = UnityEngine.Random;

namespace MiniGame
{
    public class MiniGame : MonoBehaviour
    {
        [SerializeField] private List<ClickableObject> prefabs;
        [SerializeField] Dialogo duckThanks;

        [field: SerializeField]
        public Collider Limit { get; private set; }
        
        [SerializeField] TrashBin trashBin;

        public static MiniGame instance { get; private set; }
        public static event Action OnGameFinished;

        public static event Action<string> onMessageUpdated;

        private bool isDuckCollect = false;
        
        private static Dictionary<int, bool> miniGamesState = new();
        
        private void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);               
                return;
            }
            
            instance = this;
            StartGame();
            TextBox.OnTextEnded += FinishGame;
        }

        private void OnDestroy()
        {
            TextBox.OnTextEnded -= FinishGame;
        }

        private void StartGame()
        {
            SpawnDraggableObjects();
            SpawnHiddenObject();
        }

        public void CheckObjective(HiddenObject hiddenObject)
        {
            if (!isDuckCollect)
            {
                if (hiddenObject && hiddenObject.IsCollected)
                {
                    isDuckCollect = true; 
                }
                else
                {
                    onMessageUpdated.Invoke("Arraste os lixos para encontrar e pegar o pato");
                    return;
                }
            }

            
            if (!trashBin.ContainsAllTrashs())
            {
                onMessageUpdated.Invoke("Agora coloque os lixos de volta na lixeira");
                return;
            }
            
            onMessageUpdated.Invoke("Parabéns! Você resgatou o pato.");
            TextBox.Show(duckThanks);
            OnGameFinished?.Invoke();
        }
        
        void FinishGame()
        {
            setState(MiniGameSession.currentMiniGameID, true);
            instance = null;
            GameManager.LoadGame(true);
        }
        
        static void setState(int miniGameID, bool isFinished)
        {
            miniGamesState[miniGameID] = isFinished;
        }
        
        public static bool GetState(int miniGameID)
        {
            return miniGamesState.ContainsKey(miniGameID) ? miniGamesState[miniGameID] : false;
        }
        
        private void SpawnDraggableObjects()
        {
            var spawnAmount = Random.Range(4, 6);
            
            for (var i = 0; i < spawnAmount; i++)
            {
                var randomPrefab = prefabs[Random.Range(0, prefabs.Count-1)];
                var randomPosition = new Vector3(Random.Range((float)-2, (float)2), Random.Range((float)-2, (float)1), 0);
                Instantiate(randomPrefab, randomPosition,  Quaternion.Euler(0, 0, Random.Range(0, 360)), transform);
            }
        }

        private void SpawnHiddenObject()
        {
            var randomPosition = new Vector3(Random.Range(-1, 1), Random.Range((float)-1, (float)0.5), 0);
            Instantiate(prefabs[^1], randomPosition,  Quaternion.Euler(0, 0, Random.Range(0, 360)), transform);
        }
    }
}