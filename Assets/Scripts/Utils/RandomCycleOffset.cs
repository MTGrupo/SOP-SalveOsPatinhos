using UnityEngine;

namespace Utils
{
    public class RandomCycleOffset : StateMachineBehaviour
    {
        [SerializeField] float minOffset;
        [SerializeField] float maxOffset;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetFloat("cycleOffset", Random.Range(minOffset, maxOffset));
        }
    }
}