using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

	private int hp = 20;
	private bool isDead = false;

	public EventHandler PlayerDeath;

	public override int HP {
		get { return hp; } 
		set { 
			hp = value;
		}
	}

	public override bool IsInvincible {get; set;}
	public override bool IsDead {
		get { return isDead; }
		set { isDead = value; }
	}

	private void OnEnable() {
		hp = 20;
	}


	public override void TakeDamage(int damage) {
		if(!IsInvincible) {
			hp -= damage;
			if(hp <= 0) {
				Die();
			}
		}
	}

	public override void Die() {
		OnPlayerDeath();
		isDead = true;
		transform.parent = null;
		gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("DeathFall")) {
			Die();
		}
	}

	protected virtual void OnPlayerDeath() {
		print("hello");
		if(PlayerDeath != null) {
			PlayerDeath(this, EventArgs.Empty);
		}
	}
}
