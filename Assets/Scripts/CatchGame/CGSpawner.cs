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
        
        [SerializeField] int maxDuckSpawns;
        
        int duckSpawnCount = 0;

        void SpawnDroppableObjects()
        {
            var droppableObject = DrawDroppableObjects();
            
            var randomPosition = new Vector3(Random.Range(CatchGame.Instance.Limit.bounds.min.x, CatchGame.Instance.Limit.bounds.max.x), CatchGame.Instance.Limit.bounds.max.y, 0);
            Instantiate(droppableObjects[droppableObject], randomPosition, Quaternion.identity);
        }

        int DrawDroppableObjects()
        {
            var drawValue = Random.Range(0, 11);

            if (duckSpawnCount >= maxDuckSpawns)
            {
                return Random.Range(0, droppableObjects.Count-1);
            }
            
            if (drawValue < 2)
            {
                duckSpawnCount++;
                return 2;
            }
            
            return Random.Range(0, droppableObjects.Count-1);
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