using UnityEngine;

namespace MiniGame
{
    public class MiniGameObject : MonoBehaviour
    {
        [SerializeField] private Collider objectCollider;
        [SerializeField] private SpriteRenderer sprite;
        private static List<MiniGameObject> objects = new();

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
                offset.z = (value - 1) * 0.01f;
                transform.position = offset;
            }
        }

        private static void AddObjectByIndex(int index, MiniGameObject miniGameObject)
        {
            objects.Insert(index, miniGameObject);
        }

        private static void RemoveObject(MiniGameObject miniGameObject)
        {
            objects.Remove(miniGameObject);
        }
        
        public static IReadOnlyList<MiniGameObject> GetObjects()
        {
            return objects.AsReadOnly();
        }
        
        public static int GetObjectCount()
        {
            return objects.Count;
        }
        
        public void UpdateObjectIndex(MiniGameObject trash)
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
        
        private void Awake()
        {
            Index = GetObjectCount();
            objects.Add(this);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            objectCollider = GetComponent<Collider>();
            sprite = GetComponentInChildren<SpriteRenderer>();
        }
#endif
    }
}