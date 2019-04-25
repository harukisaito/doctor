using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuddyController : MonoBehaviour {

	[SerializeField] private Transform target;
	[SerializeField] private float speed;
	[SerializeField] private float checkRadius;
	[SerializeField] private LayerMask whatIsTarget;

	private Rigidbody2D buddy;
	private SpriteRenderer spriteRenderer;

	private bool isFacingRight;
	private bool inTargetRange;
	private float movementDirection;

	private void Start() {
		buddy = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void FixedUpdate() {
		inTargetRange = Physics2D.OverlapCircle(transform.localPosition, checkRadius, whatIsTarget);

		if(target.localPosition.x < transform.localPosition.x) {
			movementDirection = -1;
		}
		else if(target.localPosition.x > transform.localPosition.x) {
			movementDirection = 1;
		}
		if(!inTargetRange) {
			buddy.velocity = new Vector2(movementDirection * speed * Time.deltaTime, 0);
		}

		if(movementDirection > 0 && !isFacingRight) {
			FlipSprite();
		}
		else if(movementDirection < 0 && isFacingRight) {
			FlipSprite();
		}
	}

	private void FlipSprite() {
		isFacingRight = !isFacingRight;
		spriteRenderer.flipX = isFacingRight;
	}
}
