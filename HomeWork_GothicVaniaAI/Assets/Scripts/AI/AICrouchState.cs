using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICrouchState : StateMachineBehaviour
{
    private MovementController movementController;
    private Transform player;
    private float rand;

    // The interesting part is in the AIMoveToPlayerState
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movementController = animator.GetComponent<MovementController>();
        
        // AI stops any movement during crouching and search for the player
        movementController.SetHorizontalMoveDirection(0);
        player = GameObject.FindWithTag("Player").transform;
        // as well it dodge our incoming damage
        animator.GetComponent<Health>().bDodge = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // AI should always look at players direction
        float directionToPlayer = player.position.x - animator.transform.position.x;
        movementController.TurnTowards(directionToPlayer);
        
        // When crouching you have 40% chance to kick, 40% to do nothing and 
        // 20% chance to stand up
        rand = Random.value;
        if (rand <= 0.4f)
        {
            animator.SetTrigger("ShouldCrouchKick");
        }
        else if (rand <= 0.6f)
        {
            animator.SetBool("ShouldCrouch", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("ShouldCrouchKick");
        
        animator.GetComponent<Health>().bDodge = false;
    }
}
