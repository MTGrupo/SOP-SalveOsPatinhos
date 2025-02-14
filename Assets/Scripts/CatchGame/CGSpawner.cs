using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CatchGame
{
    public class CGSpawner : MonoBehaviour
    {
        [SerializeField] List<Rigidbody2D> droppableObjects;
        
        [SerializeField] float spawnMinRate;
        [SerializeField] float spawnMaxRate;

        void SpawnDroppableObjects()
        {
            var topScreen = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
            
            var randomIndex = Random.Range(0, droppableObjects.Count);
            var randomPosition = new Vector3(Random.Range(CatchGame.Instance.Limit.bounds.min.x, CatchGame.Instance.Limit.bounds.max.x), topScreen+2, 0);
            Instantiate(droppableObjects[randomIndex], randomPosition, Quaternion.identity);
        }
        
        void StopSpawning()
        {
            CancelInvoke(nameof(SpawnDroppableObjects));
        }
        
        void StartSpawning()
        {
            InvokeRepeating(nameof(SpawnDroppableObjects), Random.Range(spawnMinRate, spawnMaxRate), Random.Range(spawnMinRate, spawnMaxRate));
        }

        private void OnDestroy()
        {
            CGTimer.OnTimeOver -= StopSpawning;
            CatchGame.OnGameStarted -= StartSpawning;
        }

        void Start()
        {
            CGTimer.OnTimeOver += StopSpawning;
            CatchGame.OnGameStarted += StartSpawning;
        }
    }
}