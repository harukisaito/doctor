using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlatform : MonoBehaviour {

	// private MovementController movementController;
	// private MovementController platform;


	private bool onMovingGround;

	void Start () {
		// movementController = GetComponent<MovementController>();
	}
	
	// void FixedUpdate () {
	// 	if(onMovingGround) {
	// 		// movementController.AddVelocity(platform.StartingVelocity);
	// 	}
	// }

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Platform")) {
			// platform = other.gameObject.GetComponent<MovementController>();
			transform.parent = other.transform;
			onMovingGround = true;
		}
		else if(other.gameObject.CompareTag("Ground")) {
			onMovingGround = false;
			transform.parent = null;
		}
	}
}
