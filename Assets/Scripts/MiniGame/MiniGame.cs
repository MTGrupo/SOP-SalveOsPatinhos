using System.Collections.Generic;
using Dialog;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MiniGame
{
    public class MiniGame : MonoBehaviour
    {
        [SerializeField] private List<ClickableObject> prefabs;
        [SerializeField] Dialogo duckThanks;

        [field: SerializeField]
        public Collider Limit { get; private set; }
        
        private static TrashBin trashBin => TrashBin.instance;

        public static MiniGame instance { get; private set; }
        public static event System.Action OnGameFinished;

        public UnityEvent<string> onMessageUpdated;

        private bool isDuckCollect = false;
        public static bool isFinished = false;
        
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
            isFinished = true;
            instance = null;
            GameManager.LoadGame(true);
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