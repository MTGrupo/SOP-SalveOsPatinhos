using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CatchGame
{
    public class CGSpawner : MonoBehaviour
    {
        [SerializeField] List<DroppableObject> droppableObjects;
        
        [SerializeField] float spawnMinRate;
        [SerializeField] float spawnMaxRate;

        void SpawnDroppableObjects()
        {
            var randomIndex = Random.Range(0, droppableObjects.Count);
            var randomPosition = new Vector3(Random.Range(CatchGame.Instance.Limit.bounds.min.x, CatchGame.Instance.Limit.bounds.max.x), 0, 0);
            Instantiate(droppableObjects[randomIndex], randomPosition, Quaternion.identity);
        }
        
        void StopSpawning()
        {
            CancelInvoke(nameof(SpawnDroppableObjects));
        }

        private void OnDestroy()
        {
            CGTimer.OnTimeOver -= StopSpawning;
        }

        void Start()
        {
            CGTimer.OnTimeOver += StopSpawning;
            InvokeRepeating(nameof(SpawnDroppableObjects), Random.Range(spawnMinRate, spawnMaxRate), Random.Range(spawnMinRate, spawnMaxRate));
        }
    }
}