using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour {

	private MovementController movementController;
	private MovementInputController movementInputController;

	private void Start() {
		movementController = GetComponent<MovementController>();
		movementInputController = GetComponent<MovementInputController>();
	}

	private void FixedUpdate() {
		if(!movementInputController.EnableInput) {
			movementController.Move(1.5f, 1f, 1f, 0);
			movementInputController.Sprint = false;
		}
	}
}
