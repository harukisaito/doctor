using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementController))]
public class LeftRightMovement : MonoBehaviour {

	private Rigidbody2D body;
	private MovementController movementController;
	private CheckForObstacle check;

	private float movementDirection = 1;

	private void Start() {
		body = GetComponent<Rigidbody2D>();
		movementController = GetComponent<MovementController>();
		check = GetComponent<CheckForObstacle>();
	}

	private void FixedUpdate() {
		bool obstacle = check.LookForObstacle();
		Debug.Log(obstacle);
		if(!obstacle) {
			movementDirection *= -1;
		}
		movementController.Move(false, movementDirection);
	}
}
