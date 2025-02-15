using UnityEngine;

namespace MiniGame
{
    public class TrashBin : MonoBehaviour
    {
        [SerializeField] private Collider insideTrashBin;

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
