using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class DashMovement : MonoBehaviour {

	[SerializeField] private float dashPeriod = 0.35f;
	[SerializeField] private float dashingSpeed = 300f;

	private MovementController movementController;
	private CapsuleCollider2D entityCollider;

	public EventHandler DashStart;
	public EventHandler DashEnd;

	private float dashTime;

	private void Start() {
		movementController = GetComponent<MovementController>();
		entityCollider = GetComponent<CapsuleCollider2D>();
		dashTime = dashPeriod;
	}	
	
	public void Dash(Vector2 direction, Rigidbody2D body) {
		StartCoroutine(DashCoroutine(direction, body));
	}

	private IEnumerator DashCoroutine(Vector2 direction, Rigidbody2D body) {
		OnDashStart();
		movementController.IsDashing = true;

		ChangeCapsuelOrientation(CapsuleDirection2D.Horizontal);

		while(dashTime >= 0) {
			float time = Time.deltaTime;

			dashTime -= time;
			body.velocity = direction * dashingSpeed * time;	
			yield return new WaitForFixedUpdate();
		}
		body.velocity = Vector2.zero;
		dashTime = dashPeriod;
		movementController.IsDashing = false;

		ChangeCapsuelOrientation(CapsuleDirection2D.Vertical);
		OnDashEnd();
	}

	private void ChangeCapsuelOrientation(CapsuleDirection2D direction) {
		entityCollider.direction = direction;
		entityCollider.size = new Vector2(entityCollider.size.y, entityCollider.size.x);
	}

	public void ResetColliderOrientation() {
		ChangeCapsuelOrientation(CapsuleDirection2D.Vertical);
	}

	protected virtual void OnDashStart() {
		if(DashStart != null) {
			DashStart(this, EventArgs.Empty);
		}
	}
	protected virtual void OnDashEnd() {
		if(DashEnd != null) {
			DashEnd(this, EventArgs.Empty);
		}
	}
}
