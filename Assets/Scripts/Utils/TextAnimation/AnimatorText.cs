using System.Collections;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.Utils
{
    public static class AnimatorText
    {
        static float fastTypingSpeed = 0.0001f;
        static float speedDefault = 0.05f;
        static float typingSpeed = speedDefault;

        public static IEnumerator StartAnimatorText(TextMeshProUGUI textMesh, string fullText, System.Action onFinished)
        {
            textMesh.text = "";
            ClickControllerSpeed.IsAnimationActive = true;
            
            for (int i = 0; i <= fullText.Length; i++)
            {
                textMesh.text = fullText.Substring(0, i);
                float speed = ClickControllerSpeed.IsFastForward ? fastTypingSpeed : typingSpeed;
                yield return new WaitForSeconds(speed);
            }

            onFinished?.Invoke();
            typingSpeed = speedDefault;
        }
    }
}