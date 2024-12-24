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

        public bool ContainsTrash(Bounds bounds)
        {
            return insideTrashBin.bounds.Intersects(bounds);
        }

        public bool ContainsAllTrashs()
        {
            foreach (var trash in ClickableObject.GetObjects())
            {
                if (!insideTrashBin.bounds.Intersects(trash.Bounds)) return false;
            }

            return true;
        }
        
#if UNITY_EDITOR
        private void Reset()
        {
            insideTrashBin = GetComponent<Collider>();
        }
#endif
    }
}
