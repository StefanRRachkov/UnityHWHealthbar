using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Controlls;

public class MonkCrouchState : StateMachineBehaviour
{

    private MovementController movementController;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // When you are crouching you are dodging
        
        // The only way for the AI to beat you in that moment
        // is by Jumping Kick cause with it you are going to be in Hurt state
        // where you can receive damage. 
        // Exploit!!!
        
        movementController = animator.GetComponent<MovementController>();
        
        // We stop any movements while crouching
        movementController.SetHorizontalMoveDirection(0);
        animator.GetComponent<Health>().bDodge = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(attackKey))
        {
            animator.SetTrigger("IsCrouchKicking");
        }
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

        // You stop dodging as soon as you exit
        // Crouch State
        animator.GetComponent<Health>().bDodge = false;
    }
}
