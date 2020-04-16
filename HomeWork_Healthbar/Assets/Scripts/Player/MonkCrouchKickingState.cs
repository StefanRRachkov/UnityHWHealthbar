using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Controlls;

public class MonkCrouchKickingState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // We are setting movement to 0 in the last 
        // state, so we don't need to do it here
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyUp(crouchKey))
        {
            animator.SetBool("IsCrouching", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("IsPunching");
        animator.ResetTrigger("IsCrouchKicking");
    }
}
