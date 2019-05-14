using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class LeftRightMovement : MonoBehaviour {

	private MovementController movementController;
	private CheckForObstacle check;
	private GroundCheck groundCheck;
	private ShootAtTarget shooter;

	private float movementDirectionX = 1;

	private float tempDir;

	private void Start() {
		movementController = GetComponent<MovementController>();
		check = GetComponent<CheckForObstacle>();
		groundCheck = GetComponent<GroundCheck>();
		shooter = GetComponent<ShootAtTarget>();
	}

	private void FixedUpdate() {
		bool obstacle = check.LookForObstacle();
		if(!obstacle) {
			movementDirectionX *= -1;
		}
		if(groundCheck.IsGrounded && !shooter.Shooting) {
			movementController.Move(1f, movementDirectionX, 1f, 0);
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Player")) {
			tempDir = movementDirectionX;
			movementDirectionX = 0;
		}
	}

	private void OnCollisionExit2D(Collision2D other) {
		if(other.gameObject.CompareTag("Player")) {
			movementDirectionX = tempDir;
		}
	}
}
