using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MovementController))]
public class GroundCheck : MonoBehaviour {

	[SerializeField] private Transform groundCheck;
	[SerializeField] private float checkRadius = 0.1f;	
	[SerializeField] private LayerMask whatIsGround; 

	private MovementController movementController;

	private bool isGrounded;

	public bool IsGrounded {
		get {return isGrounded;}
		set {
			isGrounded = value;
			if(movementController == null) {
				GetMovementComponent();
			}
			else if(value == true) {
				movementController.DoubleJump = true;
				movementController.Dashed = false;
			} 
		}
	}

	private void Awake() {
		GetMovementComponent();
	}

	private void FixedUpdate() {
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

		if(isGrounded) {
			movementController.DoubleJump = true; 
			movementController.Dashed = false;
		}
	}

	private void GetMovementComponent() {
		movementController = GetComponent<MovementController>();
	}
}
