using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

	private Animator animator;
	private GroundCheck groundCheck;
	private MovementController movementController;

	private void Start() {
		animator = GetComponent<Animator>();
		groundCheck = GetComponent<GroundCheck>();
		movementController = GetComponent<MovementController>();
	}

	private void Update() {
		animator.SetBool("isGrounded", groundCheck.IsGrounded);
		animator.SetFloat("yVelocity", movementController.Player.velocity.y);
	}
}
