using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageToEntity))]
public class Attack : MonoBehaviour {

	[SerializeField] private float attackTime;

	private Collider2D attackCollider;
	private SpriteRenderer spriteRenderer;
	private MovementController movementController;

	private bool isFacingRight;

	private void Awake() {
		attackCollider = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		movementController = GetComponentInParent<MovementController>();
		ActivateAttackCollider(false);
		isFacingRight = !movementController.IsFacingRight;
	}

	private void Update() {
		if(isFacingRight && movementController.MovementDirection < 0) {
			FlipCollider();
		}
		else if(!isFacingRight && movementController.MovementDirection > 0) {
			FlipCollider();
		}
	}

	public void ActivateAttack() {
		ActivateAttackCollider(true);
		StartCoroutine(DisableAttackCollider());
	}

	private void ActivateAttackCollider(bool enable) {
		attackCollider.enabled = enable;
		spriteRenderer.enabled = enable;
	}

	public void OnPlayerDeath(object src, EventArgs e) {
		ActivateAttackCollider(false);
	}

	private IEnumerator DisableAttackCollider() {
		yield return new WaitForSeconds(attackTime);
		ActivateAttackCollider(false);
	}

	private void FlipCollider() {
		isFacingRight = !isFacingRight;
		spriteRenderer.flipX = !spriteRenderer.flipX;
		Vector3 position = attackCollider.transform.localPosition;
		position.x = position.x * -1;
		attackCollider.transform.localPosition = position;
	}
}
