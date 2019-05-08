using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDirectionMovement : MonoBehaviour {

	public bool MoveRight {get; set;}
	public float Speed {get; set;} 


	private MovementController movementController;

	private float movementDirection;

	private void Start() {
		movementController = GetComponent<MovementController>();
	}

	private void FixedUpdate() {
		if(MoveRight) {
			movementDirection = 1;
		}
		else {
			movementDirection = -1;
		}
		movementController.Move(Speed, movementDirection);
	}
}
