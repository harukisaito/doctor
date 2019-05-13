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
			// movementController.AddVelocity(platform.StartingVelocity);
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Platform") {
			// platform = other.gameObject.GetComponent<MovementController>();
			Debug.Log("PARENT");
			transform.parent = other.transform;
			onMovingGround = true;
		}
		else if(other.gameObject.tag == "Ground" || !other.gameObject.activeSelf) {
			onMovingGround = false;
			transform.parent = null;
		}
	}
}
