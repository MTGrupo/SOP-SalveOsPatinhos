using UnityEngine;

namespace Utils
{
    public class ClickControllerSpeed : MonoBehaviour
    {
        public static bool IsFastForward { get; private set; }
        public static bool IsAnimationActive { get; set; }
        private void Update()
        {
            if (IsAnimationActive)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    IsFastForward = true;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    IsFastForward = false;
                }
            }
            
        }
        
    }
}