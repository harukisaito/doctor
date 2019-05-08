using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class ZigZagMovement : MonoBehaviour {

	[SerializeField] private bool moveRight;
	[SerializeField] private float speed;
	[SerializeField] private float amplitude;

	private MovementController movementController;

	private float elapsedTime;
	private float movementDirectionY = 1;
	private float movementDirectionX = 1;

	private void Start() {
		movementController = GetComponent<MovementController>();
	}

	private void FixedUpdate() {
		movementController.Move(speed, movementDirectionX, speed, movementDirectionY);
	}

	private void Update() {
		if(elapsedTime >= amplitude) {
			movementDirectionY = movementDirectionY * -1;
			elapsedTime = 0;
		}
		elapsedTime += Time.deltaTime;

		if(moveRight) {
			movementDirectionX = 1;
		}
		else {
			movementDirectionX = -1;
		}
	}

}
