using UnityEngine;

public class AIWaitState : StateMachineBehaviour {

	private MovementController movementController;
	private Transform player;
	private Health health;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		movementController = animator.GetComponent<MovementController>();
		health = animator.GetComponent<Health>();
		
		movementController.SetHorizontalMoveDirection(0);
		player = GameObject.FindWithTag("Player").transform;
	}
	
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		float directionToPlayer = player.position.x - animator.transform.position.x;
		movementController.TurnTowards(directionToPlayer);

		if (Mathf.Sin(((float) health.GetCurrentHP() / health.GetMaxHP()) * (Mathf.PI / 2)) <= 0.5f)
		{
			animator.SetBool("ShouldCrouch", true);
		}
	}
}
