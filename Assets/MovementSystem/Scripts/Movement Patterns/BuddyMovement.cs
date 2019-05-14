using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class BuddyMovement : MonoBehaviour {

	[SerializeField] private float checkRadius;
	[SerializeField] private LayerMask whatIsTarget;

	MovementController movementController;

	private bool inTargetRange;
	private float movementDirectionX;

	private void Start() {
		movementController = GetComponent<MovementController>();
	}

	private void FixedUpdate() {
		inTargetRange = Physics2D.OverlapCircle(transform.localPosition, checkRadius, whatIsTarget);

		Vector2 playerPosition = GameManager.Instance.Player.transform.localPosition;

		if(playerPosition.x < transform.localPosition.x) {
			movementDirectionX = -1;
		}
		else if(playerPosition.x > transform.localPosition.x) {
			movementDirectionX = 1;
		}

		if(!inTargetRange) {
			movementController.Move(0.8f, movementDirectionX, 0f, 0);
		}
	}
}
