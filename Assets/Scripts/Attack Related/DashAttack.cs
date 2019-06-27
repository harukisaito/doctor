using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour {

	private Collider2D dashCollider;
	private Player player;
	private Vector3 originalScale;

	private void Start() {
		dashCollider = GetComponent<Collider2D>();
		player = GameManager.Instance.Player;
		originalScale = transform.localScale;
	}

	public void OnDashStart(object source, EventArgs e) {
		transform.localScale = Vector3.one;
		Attacking(true);
	}

	public void OnDashEnd(object source, EventArgs e) {
		transform.localScale = originalScale;
		Attacking(false);
	}

	private void Attacking(bool enabled) {
		dashCollider.enabled = enabled;
		player.IsInvincible = enabled;
	}
}
