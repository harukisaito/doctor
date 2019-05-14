using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

	private GroundCheck groundCheck;
	private MovementInputController movementInput;
	private CircleCollider2D circleCollider;

	private bool moveDown;

	public DisablePlatformCollider Platform {get; set;}

	public bool MoveDown {
		get { return moveDown; } 
		set {
			moveDown = value;
			if(moveDown == true) {
				Debug.Log("DISABLE COLLIDERS");
				Platform.DisableColliders();
			}
			moveDown = false;
		}
	}

	private void Start() {
		groundCheck = GetComponent<GroundCheck>();
		movementInput = GetComponent<MovementInputController>();
		circleCollider = GetComponent<CircleCollider2D>();
	}

	private void OnCollisionStay2D(Collision2D other) {
		if(groundCheck.IsGrounded) {
			if(other.gameObject.tag == "Platform") {
				if(movementInput.MoveDown) {
					Debug.Log("MOVING DOWN");
					Platform = other.gameObject.GetComponent<DisablePlatformCollider>();
					MoveDown = true;
				}
			}
		}
	}
}

