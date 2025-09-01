using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using GraphicBehaviour = Actors.GraphicBehaviour;

namespace Duck
{
    public class DuckWithAnimation : DuckDialog
    {
        [SerializeField] private GraphicBehaviour graphicBehaviour;
        [SerializeField] private GraphicBehaviour.AnimationType animationType;
        [SerializeField] private bool initAnimation;
        [SerializeField] private bool endAnimation;
        [SerializeField] private NavMeshAgent agent;

        protected override IEnumerator Start()
        {
            if (graphicBehaviour)
            {
                graphicBehaviour.SetAnimation(animationType, initAnimation);
            }

            yield return base.Start();
        }
    
        protected override void IsRescueded()
        {
            if (agent && agent.avoidancePriority != 50) agent.avoidancePriority = 50;
            
            if (graphicBehaviour)
            {
                graphicBehaviour.SetAnimation(animationType, endAnimation);
            }

            base.IsRescueded();
        }
    }
}