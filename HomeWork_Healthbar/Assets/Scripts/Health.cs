using System;
using UnityEngine;
using static UnityEngine.Mathf;
using Random = UnityEngine.Random;

public class Health : MonoBehaviour {

	[SerializeField]
	private int maxHealth = 100;
	[SerializeField]
	private int health = 100;

	private Animator animator;
	public GameObject cross;
	
	// Better name for this boolean should be
	// immune, but for now we stay with dodge
	public bool bDodge = false;

	void Start() {
		animator = GetComponent<Animator>();
		animator.SetInteger("Health", maxHealth);
		health = maxHealth;
	}

	public int GetMaxHP()
	{
		return this.maxHealth;
	}

	public int GetCurrentHP()
	{
		return this.health;
	}

	public void SpawnCross() {
		Vector2 spawnPosition = new Vector2 {
			x = transform.position.x,
			y = -0.1f
		};
		Instantiate(cross, spawnPosition, Quaternion.identity);
	}

	public void Die() {
		Destroy(gameObject);
	}

	public void TakeDamage() {
		if (bDodge)
		{
			// We log which character is dodging at the moment
			Debug.Log(animator.tag + " Dodged");
			// and then we terminate TakeDamage() function
			return;
		}
		
		int damage = 10;
		health = Max(health - damage, 0);
		animator.SetInteger("Health", health);
		animator.SetTrigger("TookDamage");
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.transform.parent != transform
			&& collision.gameObject.CompareTag("Hitbox")) 
		{
			TakeDamage();
		}
	}
}
