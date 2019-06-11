using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

	[SerializeField] private int hp = 100;
	[SerializeField] private bool isInvincible;
	private bool isDead;

	public Enemies Key {get; set;}
	public override bool IsInvincible {
		get {return isInvincible;}
		set {isInvincible = true;}
	}

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
		if(!isInvincible) {
			hp -= damage;
			if(hp <= 0) {
				Die();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("DeathFall")) {
			Die();
		}
	}

	public override void Die() {
		isDead = true;
		ParticleManager.Instance.SpawnParticles(Particles.Attack, transform.position);
		ObjectPoolManager.Instance.AddToObjectPool(Key, gameObject);
		gameObject.SetActive(false);
	}
}
