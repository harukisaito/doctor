using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour {

	[SerializeField] private PhysicsMaterial2D noFriction;
	[SerializeField] private PhysicsMaterial2D friction;

	private CapsuleCollider2D entityCollider;
	private CircleCollider2D duckCollider;

	private void Start() {
		entityCollider = GetComponent<CapsuleCollider2D>();
		duckCollider = GetComponent<CircleCollider2D>();
	}

	public void Duck(Rigidbody2D body) {
		entityCollider.enabled = false;
		duckCollider.enabled = true;
		body.sharedMaterial = friction;
	}

	public void UnDuck(Rigidbody2D body) {
		entityCollider.enabled = true;
		duckCollider.enabled = false;
		body.sharedMaterial = noFriction;
	}
}
