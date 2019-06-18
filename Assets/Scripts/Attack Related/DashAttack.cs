using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour {

	private Collider2D dashCollider;

	private void Start() {
		dashCollider = GetComponent<Collider2D>();
	}

	public void OnDashStart(object source, EventArgs e) {
		dashCollider.enabled = true;
	}

	public void OnDashEnd(object source, EventArgs e) {
		dashCollider.enabled = false;
	}
}
