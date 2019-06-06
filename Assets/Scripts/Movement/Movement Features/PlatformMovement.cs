using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

	private MovementController movementController;

	private bool moveDown;

	private DisablePlatformCollider platform;

	public bool MoveDown {
		set {
			moveDown = value;
			if(moveDown == true) {
				platform.AllowMovingDown();
			}
			moveDown = false;
		}
	}

	private void Start() {
		movementController = GetComponent<MovementController>();
	}

	private void OnCollisionStay2D(Collision2D other) {
		if(movementController.MovingDown) {
			if(other.gameObject.CompareTag("Platform")) {
				platform = other.gameObject.GetComponent<DisablePlatformCollider>();
				MoveDown = true;
			}
		}
	}
}

