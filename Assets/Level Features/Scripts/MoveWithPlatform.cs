using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlatform : MonoBehaviour {

	private MovementController movementController;
	private MovementController platform;

	private bool onMovingGround;

	void Start () {
		movementController = GetComponent<MovementController>();
	}
	
	void FixedUpdate () {
		if(onMovingGround) {
			movementController.AddVelocity(platform.StartingVelocity);
			// Debug.Log("PLATFORM = " + platform.Velocity);
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Flying Object") {
			platform = other.gameObject.GetComponent<MovementController>();
			onMovingGround = true;
		}
		else if(other.gameObject.tag == "Ground") {
			onMovingGround = false;
		}
	}
}
