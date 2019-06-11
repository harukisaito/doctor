using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class LeftRightMovement : MonoBehaviour {

	private MovementController movementController;
	private CheckForObstacle check;
	private GroundCheck groundCheck;

	private float movementDirectionX = -1;

	private void Start() {
		movementController = GetComponent<MovementController>();
		check = GetComponent<CheckForObstacle>();
		groundCheck = GetComponent<GroundCheck>();
	}

	private void FixedUpdate() {
		bool obstacle = check.LookForObstacle();
		bool wall = check.LookForWall();
		if((!obstacle || wall) && groundCheck.IsGrounded) {
			movementDirectionX *= -1;
			check.CheckPositionX *= -1;
		}
		if(groundCheck.IsGrounded) {
			movementController.Move(1f, movementDirectionX, 1f, 0);
		}
	}
}
