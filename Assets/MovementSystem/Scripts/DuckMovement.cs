using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour {

	private CapsuleCollider2D entityCollider;
	private CircleCollider2D duckCollider;

	private void Start() {
		entityCollider = GetComponent<CapsuleCollider2D>();
		duckCollider = GetComponent<CircleCollider2D>();
	}

	public void Duck(Rigidbody2D body) {
		entityCollider.enabled = false;
		duckCollider.enabled = true;
		body.velocity = new Vector2(0, body.velocity.y);
	}

	public void UnDuck(Rigidbody2D body) {
		entityCollider.enabled = true;
		duckCollider.enabled = false;
	}
}
