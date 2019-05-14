﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageToEntity))]
public class SwingAttack : MonoBehaviour {

	[SerializeField] private KeyCode attackButton;
	[SerializeField] private float attackTime;

	private Collider2D boxCollider;
	private SpriteRenderer spriteRenderer;
	private MovementController movementController;

	private float timer;
	private bool isFacingRight;

	private void Awake() {
		boxCollider = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		movementController = GetComponentInParent<MovementController>();
		boxCollider.enabled = false;
		spriteRenderer.enabled = false;
		isFacingRight = !movementController.IsFacingRight;
	}

	private void Update() {
		if(Input.GetKeyDown(attackButton) && !boxCollider.enabled) {
			boxCollider.enabled = true;
			spriteRenderer.enabled = true;
			timer = 0;
		}
		if(timer <= attackTime && boxCollider.enabled) {
			timer += Time.deltaTime;
			if(timer >= attackTime) {
				timer = 0;
				boxCollider.enabled = false;
				spriteRenderer.enabled = false;
			}
		}

		if(isFacingRight && movementController.MovementDirection < 0) {
			FlipCollider();
		}
		else if(!isFacingRight && movementController.MovementDirection > 0) {
			FlipCollider();
		}
	}

	private void FlipCollider() {
		isFacingRight = !isFacingRight;
		Vector3 position = boxCollider.transform.localPosition;
		position.x = position.x * -1;
		boxCollider.transform.localPosition = position;
	}
}