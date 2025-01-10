using System.Collections;
using UnityEngine;
using GraphicBehaviour = Actors.GraphicBehaviour;

namespace Duck
{
    public class DuckWithAnimation : DuckDialog
    {
        [SerializeField] private GraphicBehaviour graphicBehaviour;
        [SerializeField] private GraphicBehaviour.AnimationType animationType;
        [SerializeField] private bool initAnimation;
        [SerializeField] private bool endAnimation;

        protected override IEnumerator Start()
        {
            if (graphicBehaviour != null)
            {
                graphicBehaviour.SetAnimation(animationType, initAnimation);
            }
            else
            {
                Debug.LogWarning("GraphicBehaviour is null");
            }

            yield return base.Start();
        }
    
        protected override void IsRescueded()
        {
            if (graphicBehaviour != null)
            {
                graphicBehaviour.SetAnimation(animationType, endAnimation);
            }
            else
            {
                Debug.LogWarning("GraphicBehaviour is null");
            }

            base.IsRescueded();
        }
    }
}