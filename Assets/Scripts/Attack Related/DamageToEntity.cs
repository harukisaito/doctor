using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToEntity : MonoBehaviour {

	[SerializeField] private int damage;

	private Entity entity;

	private Quaternion enemyRotation;
	private Quaternion playerRightRotation;
	private Quaternion playerLeftRotation;

	private void Start() {
		enemyRotation = Quaternion.Euler(0, 0, 135);
		playerRightRotation = Quaternion.Euler(0, 0, 315);
		playerLeftRotation = Quaternion.Euler(0, 0, 135);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if((other.CompareTag("Player") && this.gameObject.tag != "PlayerAttack") 
		|| (other.CompareTag("Enemy") && this.gameObject.tag != "EnemyAttack")
		|| (other.CompareTag("EnemyAttack") && this.gameObject.CompareTag("PlayerAttack"))) {
				   
			entity = other.GetComponent<Entity>();
			
			if(entity == null) {
				return;
			}

			entity.TakeDamage(damage);

			SpawnParticles(other);
		}
	}

	private void SpawnParticlesProjectile(Collider2D other) {
		ParticleManager.Instance.SpawnParticles(Particles.ProjectileDestrucion, other.transform.position, Quaternion.identity);
	}

	private void SpawnParticles(Collider2D other) {
		float otherX = other.transform.position.x;
		float thisX = transform.position.x;
		Quaternion rotation;

		if(entity.ParticleTypeDamage == Particles.PlayerDamage) {
			if(otherX < thisX) {
				rotation = playerLeftRotation;
			}
			else {
				rotation = playerRightRotation;
			}
		}
		else {
			if(otherX < thisX) {
				rotation = enemyRotation;
			}
			else {
				rotation = Quaternion.identity;
			}
		}
		ParticleManager.Instance.SpawnParticles(entity.ParticleTypeDamage, other.transform.position, rotation);
		entity = null;
	}

	

}
