using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

	private Animator animator;
	private GroundCheck groundCheck;
	private MovementController movement;
	private MovementInputController movementInput;

	private void Start() {
		animator = GetComponent<Animator>();
		groundCheck = GetComponent<GroundCheck>();
		movement = GetComponent<MovementController>();
		movementInput = GetComponent<MovementInputController>();
	}

	private void Update() {
		animator.SetBool("isGrounded", groundCheck.IsGrounded);
		animator.SetFloat("yVelocity", movement.Velocity.y);
		animator.SetFloat("xVelocity", Mathf.Abs(movement.Velocity.x));
		animator.SetBool("isSprinting", movementInput.Sprint);
		animator.SetBool("isDashing", movement.IsDashing);
		animator.SetBool("isDucking", movement.IsDucking); 
	}
}
