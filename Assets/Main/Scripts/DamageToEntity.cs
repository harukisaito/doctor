using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToEntity : MonoBehaviour {

	[SerializeField] private int damage;
	[SerializeField] private bool haveInvincibilityPeriod;
	[SerializeField] private float invincibiltyPeriod = 0.5f;

	private Entity entity;
	private InvincibleWhenHit invincibleEntity;

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player" || (other.gameObject.tag == "Enemy" && this.gameObject.tag != "EnemyAttack")) {
			entity = other.GetComponent<Entity>();

			entity.TakeDamage(damage);
			
			if(haveInvincibilityPeriod && !entity.IsInvincible) {
				if(invincibleEntity == null) {
					invincibleEntity = other.GetComponent<InvincibleWhenHit>();
				}
				invincibleEntity.StartInvincibility(invincibiltyPeriod);
			}
		}
		else if(other.gameObject.tag == "Untagged") {
			Debug.LogWarning("CHECK THE TAG OF THE OTHER OBJECT");
		}
	}
}
