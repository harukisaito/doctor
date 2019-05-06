using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageToEntity))]
public class SwingAttack : MonoBehaviour {

	[SerializeField] private KeyCode attackButton;
	[SerializeField] private float attackTime;

	private Collider2D halfCircleCollider;
	private SpriteRenderer spriteRenderer;
	private MovementController movementController;

	private float timer;
	private bool isFacingRight;

	private void Awake() {
		halfCircleCollider = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		movementController = GetComponentInParent<MovementController>();
		halfCircleCollider.enabled = false;
		spriteRenderer.enabled = false;
		isFacingRight = !movementController.IsFacingRight;
	}

	private void Update() {
		if(Input.GetKeyDown(attackButton) && !halfCircleCollider.enabled) {
			halfCircleCollider.enabled = true;
			spriteRenderer.enabled = true;
			timer = 0;
		}
		if(timer <= attackTime && halfCircleCollider.enabled) {
			timer += Time.deltaTime;
			if(timer >= attackTime) {
				timer = 0;
				halfCircleCollider.enabled = false;
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
		Vector3 scale = transform.localScale;
		scale.x = scale.x * -1;
		transform.localScale = scale;
	}
}
