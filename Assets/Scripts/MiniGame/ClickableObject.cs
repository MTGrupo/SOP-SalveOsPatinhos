using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace MiniGame
{
    public class ClickableObject : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D objectCollider;
        [SerializeField] private SortingGroup sprite;
        
        private static List<ClickableObject> clickableObjects = new();

        private int index;
        
        public Bounds Bounds => objectCollider.bounds;

        public int Index
        {
            get => index;
            set
            {
                index = value;
                transform.SetSiblingIndex(index);
                sprite.sortingOrder = -value;
                var offset = transform.position;
                offset.z = (value - 10) * 0.1f;
                transform.position = offset;
            }
        }

        private static void AddObjectByIndex(int index, ClickableObject clickableObject)
        {
            clickableObjects.Insert(index, clickableObject);
        }

        private static void RemoveObject(ClickableObject clickableObject)
        {
            clickableObjects.Remove(clickableObject);
        }
        
        public static IReadOnlyList<ClickableObject> GetObjects()
        {
            return clickableObjects.AsReadOnly();
        }
        
        public static int GetObjectCount()
        {
            return clickableObjects.Count;
        }
        
        public void UpdateObjectIndex(ClickableObject trash)
        {
            RemoveObject(trash);
            AddObjectByIndex(0, trash);

            ReorderObjectsIndex();
        }
        
        private static void ReorderObjectsIndex()
        {
            var currentIndex = 0;
        
            foreach (var miniGameObject in GetObjects())
            {
                miniGameObject.Index = currentIndex++;
            }
        }
        
        private IEnumerable<Collider2D> GetOverlappingColliders()
        {
            Vector2 center = (Vector2)objectCollider.transform.position + (Vector2)(objectCollider.transform.rotation * objectCollider.offset);
            Vector2 size = objectCollider.size * objectCollider.transform.lossyScale;
            float angle = objectCollider.transform.eulerAngles.z;
    
            Collider2D[] colliders = Physics2D.OverlapBoxAll(center, size, angle);
    
            foreach (var collider in colliders)
            {
                if (collider == objectCollider) continue;
                if (collider.transform.position.z < objectCollider.transform.position.z)
                {
                    yield return collider;
                }
            }
        }
        public bool IsSuperimposed()
        {
            return GetOverlappingColliders().Any();
        }

        public void AlertSuperimposing()
        {
            foreach (var collider in GetOverlappingColliders())
            {
                var objectDragAndDrop = collider.GetComponent<IDragAndDrop>();
                objectDragAndDrop?.OnSuperimposing?.Invoke();
            }
        }
        
        private void Awake()
        {
            Index = GetObjectCount();
            clickableObjects.Add(this);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            objectCollider = GetComponent<BoxCollider2D>();
            sprite = GetComponentInChildren<SortingGroup>();
        }
#endif
    }
}