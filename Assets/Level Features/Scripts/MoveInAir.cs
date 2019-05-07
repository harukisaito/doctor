using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInAir : MonoBehaviour {

	private GroundCheck groundCheck;
	private MovementController movementController;
	private FlyingBehaviour platform;

	private bool onMovingGround;

	void Start () {
		groundCheck = GetComponent<GroundCheck>();
		movementController = GetComponent<MovementController>();
	}
	
	void FixedUpdate () {
		if(!groundCheck.IsGrounded && onMovingGround && movementController.DoubleJump) {
			if(platform != null) {
				if(!platform.MoveRight) {
					Debug.Log("Moving Left");
					transform.localPosition += Vector3.left * platform.Speed * Time.fixedDeltaTime;
				}
				else if(platform.MoveRight) {
					Debug.Log("Moving Right");
					transform.localPosition += Vector3.right * platform.Speed * Time.fixedDeltaTime;
				}
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Flying Object") {
			platform = other.gameObject.GetComponent<FlyingBehaviour>();
			onMovingGround = true;
		}
		else if(other.gameObject.tag == "Ground") {
			onMovingGround = false;
		}
	}
}
