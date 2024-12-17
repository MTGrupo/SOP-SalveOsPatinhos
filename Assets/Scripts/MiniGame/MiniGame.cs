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
        [SerializeField] private List<MiniGameObject> prefabs;
        [SerializeField] Dialogo duckThanks;

        [field: SerializeField]
        public Collider Limit { get; private set; }
        
        private static TrashBinBehaviour TrashBin => TrashBinBehaviour.Instance;

        public static MiniGame Instance { get; private set; }
        public static event System.Action OnGameFinished;

        public UnityEvent<string> onMessageUpdated;

        private bool isDuckCollect = false;
        public static bool isFinished = false;
        
        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);               
                return;
            }
            
            Instance = this;
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

            
            if (!trashBin.ContainsAllObjects())
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
            Instance = null;
            GameManager.LoadGame(true);
        }

        public bool IsObjectSuperimposed(Bounds bounds, int index)
        {
            for (var i = index - 1; i >= 0; i--)
            {
                if (bounds.Intersects(objects[i].Bounds) && objects[i].Bounds != bounds)
                    return true;
            }
        
            return false;
        }
        
        public void AlertSuperimposing(Bounds bounds, int index)
        {
            for (var i = index - 1; i >= 0; i--)
            {
                if (bounds.Intersects(objects[i].Bounds) && objects[i].Bounds != bounds)
                {
                    var objectDragAndDrop = objects[i].GetComponent<IDragAndDrop>();
                    objectDragAndDrop?.OnSuperimposing.Invoke();
                }
            }
        }
        
        
        private void SpawnDraggableObjects()
        {
            var spawnAmount = Random.Range(8, 12);
            
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