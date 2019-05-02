using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class LeftRightMovement : MonoBehaviour {

	private MovementController movementController;
	private CheckForObstacle check;
	private GroundCheck groundCheck;
	private ShootAtTarget shooter;

	private float movementDirection = 1;

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
			movementDirection *= -1;
		}
		if(groundCheck.IsGrounded && !shooter.Shooting) {
			movementController.Move(1f, movementDirection);
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			tempDir = movementDirection;
			movementDirection = 0;
		}
	}

	private void OnCollisionExit2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			movementDirection = tempDir;
		}
	}
}
