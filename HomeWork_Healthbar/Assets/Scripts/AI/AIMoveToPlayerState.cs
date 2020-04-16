using UnityEngine;
using static UnityEngine.Mathf;

public class AIMoveToPlayerState : StateMachineBehaviour {

	[SerializeField]
	[Range(0.1f, 0.4f)]
	private float wantedDistanceToPlayer = 0.2f;

	private Transform player;
	private MovementController movementController;
	private Health health;

	private float rand;
	// dangerMeter will describe the need of dodging or attacking 
	private float dangerMeter;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		GameObject playerGameObject = GameObject.FindWithTag("Player");
		if (playerGameObject == null) 
		{
			Debug.LogError("No GameObject with the \"Player\" tag found");
		} else 
		{
			player = playerGameObject.transform;
		}

		movementController = animator.GetComponent<MovementController>();
		health = animator.GetComponent<Health>();
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		Vector3 vectorToPlayer = player.position - animator.transform.position;
		float distanceToPlayer = vectorToPlayer.magnitude;
		
		// If the player isn't in range the AI is moving towards him
		if (distanceToPlayer > wantedDistanceToPlayer) 
		{
			movementController.SetHorizontalMoveDirection(Sign(vectorToPlayer.x));
		} 
		// When the player is in range the AI chooses what to do 
		else
		{
			// I want to have some control on the AI behaviour when choosing
			// using curves we will decide what is better option for our AI
			// If its health is low it will more frequently decide to crouch
			// if its health is higher it will punch more
			
			// The basic concept is - AI will have 20% chance to do something 
			// random. The other 95% are going to be strictly evaluated with 
			// Mathf.Sin function
			
			// The 20% chance for random choice
			// this randomness is making a good balance between 
			// high HP boss and our player
			rand = Random.value;
			if (rand <= (health.GetMaxHP() / 1000.0f))
			{
				rand = Random.value;
				if(rand <= 0.5f) animator.SetBool("ShouldPunch", true);
				else animator.SetBool("ShouldCrouch", true);
			}
			else
			{
				// (health.GetCurrentHP() / health.GetMaxHP()) * (Mathf.PI / 2)
				// this function will give us
				// float between 0 and 1 
				// we use Sin 'cause it's not fixed value like 50% of the HP
				dangerMeter = Mathf.Sin(((float)health.GetCurrentHP() / health.GetMaxHP()) * (Mathf.PI / 2));
				Debug.Log(dangerMeter);
				if(dangerMeter >= 0.5f) animator.SetBool("ShouldPunch", true);
				else animator.SetBool("ShouldCrouch", true);
			}
		}
	}
}
