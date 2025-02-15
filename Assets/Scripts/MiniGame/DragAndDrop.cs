using UnityEngine;
using UnityEngine.Events;

namespace MiniGame
{
    public class DragAndDrop : MonoBehaviour, IDragAndDrop
    {
        [SerializeField] private ClickableObject draggableObject;
        [SerializeField] private AudioSource trashSound;
        public AudioClip dragged, dropped, superimposed;
        [field: SerializeField] public UnityEvent<bool, bool> OnTouched { get; private set; }
        [field: SerializeField] public UnityEvent OnSuperimposed { get; private set; }
        [field: SerializeField] public UnityEvent OnSuperimposing { get; private set; }
        
        public Vector3 originalPosition;
        
        private bool isDragging;
        private bool isOnTrashBin;
        private bool isSuperimposed;

        [SerializeField] TrashBin trashBin;
        private static MiniGame miniGame => MiniGame.instance;
        
        public bool IsDragging
        {
            get => isDragging;
            private set
            {
                isDragging = value;
                OnTouched.Invoke(isDragging, isOnTrashBin);
            }
        }
        
        public bool IsSuperimposed
        {
            get => isSuperimposed;
            private set
            {
                isSuperimposed = value;

                if (!value) return;
                
                SuperimposingMessage.Show();
                trashSound.PlayOneShot(superimposed);
                OnSuperimposed.Invoke();
                draggableObject.AlertSuperimposing();
            }
        }
        
        void OnGameFinished()
        {
            enabled = false;
        }

        void OnDestroy()
        {
            MiniGame.OnGameFinished -= OnGameFinished;
        }

        private void Start()
        {
            originalPosition = draggableObject.transform.position;
            MiniGame.OnGameFinished += OnGameFinished;
        }

        private void Update()
        {
            if (!IsDragging) return;
        
            var mousePosition = GetMousePosition();
            mousePosition.z = draggableObject.transform.position.z;
            
            if(!MiniGame.instance.Limit.bounds.Contains(mousePosition))
                return;

            draggableObject.transform.position = mousePosition;
        }
        
        private void OnMouseDown()
        {
            if(!enabled)
                return;
            
            IsSuperimposed = draggableObject.IsSuperimposed();
            
            if (IsSuperimposed) return;
            
            trashSound.PlayOneShot(dragged);
            
            IsDragging = true;
            
            draggableObject.UpdateObjectIndex(draggableObject);
        }
        
        private void OnMouseUp()
        {
            if(!enabled)
                return;
            
            isOnTrashBin = trashBin.ContainsTrash(draggableObject.Bounds);
            
            if(!IsSuperimposed)
                trashSound.PlayOneShot(dropped);
            
            IsDragging = false;

            if (isOnTrashBin)
                draggableObject.transform.position = new Vector3(originalPosition.x, originalPosition.y, draggableObject.transform.position.z);
            
            miniGame.CheckObjective(null);
        }
        
        private static Vector3 GetMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        void Awake()
        {
            trashBin = FindObjectOfType<TrashBin>();
        }

#if UNITY_EDITOR
        private void Reset()
        {
            draggableObject = GetComponent<ClickableObject>();
            trashSound = GetComponent<AudioSource>();
        }
#endif
    }
}