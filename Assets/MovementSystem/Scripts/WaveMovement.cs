using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class WaveMovement : MonoBehaviour {

	[SerializeField] private bool moveRight;
	[SerializeField] private float speedX;
	[SerializeField] private float amplitude = 5f;
	[SerializeField] private float frequency = 1f;

	private MovementController movementController;

	private float elapsedTime;
	private float movementDirectionY = 1;
	private float movementDirectionX = 1;
	private float speedY;

	private void Start() {
		movementController = GetComponent<MovementController>();
	}

	private void FixedUpdate() {
		movementController.Move(speedX, movementDirectionX, speedY, movementDirectionY);
	}

	private void Update() {
		if(elapsedTime >= frequency) {
			movementDirectionY = movementDirectionY * -1;
			elapsedTime = 0;
		}
		elapsedTime += Time.deltaTime;
		speedY = (amplitude * elapsedTime) - ((elapsedTime * elapsedTime) * amplitude);

		if(moveRight) {
			movementDirectionX = 1;
		}
		else {
			movementDirectionX = -1;
		}
	}

}
