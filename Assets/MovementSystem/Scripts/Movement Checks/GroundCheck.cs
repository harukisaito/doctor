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
		set {isGrounded = value;}
	}


	private void Awake() {
		GetComponents();
	}

	private void FixedUpdate() {
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

		if(isGrounded) {
			movementController.DoubleJump = true; 
			movementController.Dashed = false;
			if(!movementController.DisableKnockback) {
				movementController.KnockBacked = false;
			}
		}
	}

	private void GetComponents() {
		movementController = GetComponent<MovementController>();
	}
}
