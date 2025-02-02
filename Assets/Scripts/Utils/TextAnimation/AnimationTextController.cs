using TMPro;
using UnityEngine;

namespace Utils
{ 
    public class AnimationTextController : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI text { get; private set; } 
        
        [SerializeField] private float speed = 0.09f;
        [SerializeField] private int stepLetters = 1;
        [SerializeField] private bool useClickedFastAnimation = false;
        [SerializeField] private Alignment alignment = Alignment.Left;
        
        private string fullText;
        private void Start()
        {
            fullText = text.text;
            AnimatorText.AnimationSettings(speed: speed, step: stepLetters, align: alignment, useClicked: useClickedFastAnimation);
            StartCoroutine(AnimatorText.StartAnimatorText(text, fullText));
        }
        
#if UNITY_EDITOR
        private void Reset()
        {
            if (text == null)
            {
                text = GetComponentInChildren<TextMeshProUGUI>();
            }
                
        }
#endif
    }
}