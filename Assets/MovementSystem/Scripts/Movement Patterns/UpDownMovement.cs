using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class UpDownMovement : MonoBehaviour {

	[SerializeField] private float speed;
	[SerializeField] private float flyingPeriod;

	private MovementController movementController;

	private float elapsedTime;
	private float movementDirectionY = 1;

	private void Start() {
		movementController = GetComponent<MovementController>();
	}

	private void FixedUpdate() {
		movementController.Move(0f, 0f, speed, movementDirectionY);
	}

	private void Update() {
		if(elapsedTime >= flyingPeriod) {
			movementDirectionY = movementDirectionY * -1;
			elapsedTime = 0;
		}
		elapsedTime += Time.deltaTime;
	}
}
