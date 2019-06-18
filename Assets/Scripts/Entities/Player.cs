using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

	private int hp = 20;
	private bool isDead = false;

	public EventHandler PlayerDeath;
	private Coroutine invincibility;

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
		hp = 20;
		if(invincibility != null) {
			StopCoroutine(invincibility);
		}
		if(PlayerDeath != null) {
			PlayerDeath(this, EventArgs.Empty);
		}
	}

	public void OnDashStart(object source, EventArgs e) {
		IsInvincible = true;
	}

	public void OnDashEnd(object source, EventArgs e) {
		invincibility = StartCoroutine(DisableInvincibility());
	}

	private IEnumerator DisableInvincibility() {
		yield return new WaitForSeconds(0.2f);
		IsInvincible = false;
	}
}
