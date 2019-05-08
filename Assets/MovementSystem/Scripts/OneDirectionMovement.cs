using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class OneDirectionMovement : MonoBehaviour {

	public bool MoveRight {get; set;}
	public float Speed {get; set;} 


	private MovementController movementController;

	private float movementDirectionX;

	private void Start() {
		movementController = GetComponent<MovementController>();
	}

	private void FixedUpdate() {
		if(MoveRight) {
			movementDirectionX = 1;
		}
		else {
			movementDirectionX = -1;
		}
		movementController.Move(Speed, movementDirectionX, 1f, 0);
	}
}
