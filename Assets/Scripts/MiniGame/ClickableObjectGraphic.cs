using UnityEngine;

namespace MiniGame
{
    public class ClickableObjectGraphic : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [Header("Parametros")]
        [SerializeField] private string dragParameter;
        [SerializeField] private string trashBinParameter;
        [SerializeField] private string superimposingTrigger;
        [SerializeField] private string superimposedTrigger;
        [SerializeField] private DragAndDrop dragAndDrop;

        private IDragAndDrop iDragAndDrop;

        private void Start()
        {
            IsOnTrashBin = true;
        }

        public bool IsDragging
        {
            get => animator.GetBool(dragParameter);
            set => animator.SetBool(dragParameter, value);
        }
        
        public bool IsOnTrashBin
        {
            get => animator.GetBool(trashBinParameter);
            set => animator.SetBool(trashBinParameter, value);
        }
        
        private void OnInteracted(bool isDragging, bool isOnTrashBin)
        {
            IsDragging = isDragging;
            IsOnTrashBin = isOnTrashBin;
        }
        
        private void AlertSuperimposing()
        {
            animator.SetTrigger(superimposingTrigger);
        }
        
        private void AlertSuperimposed()
        {
            animator.SetTrigger(superimposedTrigger);
        }
        
        private void Awake()
        {
            iDragAndDrop = dragAndDrop.GetComponent<IDragAndDrop>();
            dragAndDrop?.OnTouched.AddListener(OnInteracted);
            dragAndDrop?.OnSuperimposed.AddListener(AlertSuperimposed);
            dragAndDrop?.OnSuperimposing.AddListener(AlertSuperimposing);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            animator = GetComponentInParent<Animator>(true);
        }
#endif
    }
}