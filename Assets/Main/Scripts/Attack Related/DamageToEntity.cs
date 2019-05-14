using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToEntity : MonoBehaviour {

	[SerializeField] private int damage;
	// [SerializeField] private bool haveInvincibilityPeriod;
	// [SerializeField] private float invincibiltyPeriod = 0.5f;

	private Entity entity;
	// private InvincibleWhenHit invincibleEntity;
	// private MovementController movementController;

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player") || (other.gameObject.CompareTag("Enemy") && this.gameObject.tag != "EnemyAttack")) {
			entity = other.GetComponent<Entity>();
			
			entity.TakeDamage(damage);

			ParticleManager.Instance.InstantiateParticles(other.transform.position);
			// if(haveInvincibilityPeriod && !entity.IsInvincible) {
			// 	if(invincibleEntity == null) {
			// 		invincibleEntity = other.GetComponent<InvincibleWhenHit>();
			// 	}
			// 	invincibleEntity.StartInvincibility(invincibiltyPeriod);
			// }
			// if(other.gameObject.CompareTag("Player")) {
				// movementController = other.GetComponent<MovementController>();
				// if(other.gameObject.transform.position.x < transform.position.x) {
				// 	Knockback(KnockbackDirection.Left);
				// } else if(other.gameObject.transform.position.x > transform.position.x) {
				// 	Knockback(KnockbackDirection.Right);
				// }
			// }
		}
		else if(other.gameObject.CompareTag("Untagged")) {
			Debug.LogWarning("CHECK THE TAG OF THE OTHER OBJECT");
		}
	}

	// private void Knockback(KnockbackDirection direction) {
	// 	movementController.Knockback(direction, 3, 2);
	// }
}
