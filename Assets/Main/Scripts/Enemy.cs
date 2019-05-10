using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

	private int hp = 100;
	private bool isDead;

	public Keys Key {get; set;}
	public override bool IsInvincible {get; set;}

	public override int HP {
		get { return hp; } 
		set { 
			hp = value;
			TakeDamage(value); 

		}
	}

	public override bool IsDead {
		get { return isDead; }
		set {
			isDead = value; 
		}
	}

	public override void TakeDamage(int damage) {
		if(!IsInvincible) {
			hp -= damage;
			if(hp <= 0) {
				Die();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "DeathFall") {
			Die();
		}
	}

	public override void Die() {
		isDead = true;
		ParticleManager.Instance.InstantiateParticles(transform.position, 0.2f);
		gameObject.SetActive(false);
		ObjectPoolManager.Instance.AddToObjectPool(Key, gameObject);
	}
}
