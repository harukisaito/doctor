using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlatform : MonoBehaviour {

	// private GroundCheck groundCheck;
	private MovementController movementController;
	private MovementController platform;

	private bool onMovingGround;

	void Start () {
		// groundCheck = GetComponent<GroundCheck>();
		movementController = GetComponent<MovementController>();
	}
	
	void FixedUpdate () {
		if(onMovingGround) {
			movementController.AddVelocity(platform.StartingVelocity);
			Debug.Log("PLATFORM = " + platform.Velocity);
			// if(platform != null) {
			// 	if(!platform.MoveRight) {
			// 		Debug.Log("Moving Left");
			// 		transform.localPosition += Vector3.left * platform.Speed * Time.fixedDeltaTime;
			// 	}
			// 	else if(platform.MoveRight) {
			// 		Debug.Log("Moving Right");
			// 		transform.localPosition += Vector3.right * platform.Speed * Time.fixedDeltaTime;
			// 	}
			// }
			// movementController.AddVelocity(platform.Player.velocity);
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Flying Object") {
			platform = other.gameObject.GetComponent<MovementController>();
			onMovingGround = true;

			// Debug.Log("PLATFORM = " + platform.StartingVelocity);
		}
		else if(other.gameObject.tag == "Ground") {
			// onMovingGround = false;
		}
	}
}
