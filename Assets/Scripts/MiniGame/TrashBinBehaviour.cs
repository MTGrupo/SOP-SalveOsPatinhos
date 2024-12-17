using UnityEngine;

namespace MiniGame
{
    public class TrashBin : MonoBehaviour
    {
        public static TrashBin instance { get; private set; }
        [SerializeField] private Collider insideTrashBin;
    
        private void Awake()
        {
            if (!instance)
            {
                instance = this;
                return;
            }
        
            Destroy(gameObject);
        }

        public bool ContainsObject(Bounds bounds)
        {
            return insideTrashBin.bounds.Intersects(bounds);
        }
        
#if UNITY_EDITOR
        private void Reset()
        {
            insideTrashBin = GetComponent<Collider>();
        }
#endif
    }
}
