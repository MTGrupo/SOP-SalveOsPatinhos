using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Utils
{
    public static class AnimatorText
    {
        static float fastTypingSpeed = 0.0001f;
        static float speedDefault = 0.05f;
        static float typingSpeed = speedDefault;
        static int letterStep = 1;
        static string alignAnimationText = "left";
        
        public static void AnimationSettings(
            float speed = 0.05f, 
            int step = 1, 
            Alignment align = Alignment.Left)
        {
            if (speed <= 0) speed = speedDefault;
            if (step <= 0) step = letterStep;
            
            typingSpeed = speed;
            letterStep = step;
            alignAnimationText = align.ToString().ToLower();
        }
        
        public static IEnumerator StartAnimatorText(TextMeshProUGUI textMesh, string fullText, Action onFinished = null)
        {
            Debug.Log("Animação de texto começou");  

            textMesh.text = "";
            int length = fullText.Length;

            yield return AnimateTextByAlignment(textMesh, fullText, length);

            Debug.Log("Animação de texto terminou");  

            onFinished?.Invoke();
            typingSpeed = speedDefault;
        }

        private static IEnumerator AnimateTextByAlignment(TextMeshProUGUI textMesh, string fullText, int length)
        {
            switch (alignAnimationText)
            {
                case "right":
                    yield return AnimateTextRight(textMesh, fullText, length);
                    break;
                case "left":
                default:
                    yield return AnimateTextLeft(textMesh, fullText, length);
                    break;
            }
        }

        private static IEnumerator AnimateTextLeft(TextMeshProUGUI textMesh, string fullText, int length)
        {
            for (int i = 0; i <= fullText.Length; i += letterStep)
            {
                textMesh.text = fullText.Substring(0, i);
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        private static IEnumerator AnimateTextRight(TextMeshProUGUI textMesh, string fullText, int length)
        {
            string animatedText = new string(' ', length); 

            for (int i = 0; i <= length; i += letterStep)
            {
                animatedText = animatedText.Substring(0, length - i) + fullText.Substring(length - i, i);
                textMesh.text = animatedText;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }
}
