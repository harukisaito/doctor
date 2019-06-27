using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MovementController))]
public class GroundCheck : MonoBehaviour {

	[SerializeField] private Transform groundCheck;
	[SerializeField] private float checkRadius = 0.1f;	
	[SerializeField] private LayerMask whatIsGround; 
	[SerializeField] private float groundedDelay;

	private MovementController movementController;
	private DropShadow dropShadow;

	public EventHandler Landed;

	private bool reset;
	private bool physicsGrounded;
	private bool isGrounded;

	private bool inAir;
	private bool inJump;
	public bool IsGrounded {
		get {return physicsGrounded;}
	}

	public bool InAir {
		set {
			inAir = value;
			if(dropShadow != null) {
				dropShadow.EnableDropShadow = !value;
			}
		}
	}

	public bool CanJump {
		get {
			// print("in getter: grounded = " + isGrounded);
			return isGrounded;}
		set {
			isGrounded = value;
			inJump = true;
			StartCoroutine(TurnCollisionsBackOn());
		}
	}


	private void Awake() {
		GetComponents();
	}

	private void FixedUpdate() {
		if(!movementController.IsDashing && !inJump) {
			physicsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
		} else physicsGrounded = false;

		if(physicsGrounded) {
			if(!reset) {
				StartCoroutine(ResetPlayerAbilites());
			}
		}
	}

	private void OnDrawGizmos() {
		Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
	}

	private void Update() {
		if(!physicsGrounded) {
			if(!inAir) {
				reset = false;
				isGrounded = false;
				InAir = true;
			}
		}
	}

	private IEnumerator TurnCollisionsBackOn() {
		yield return new WaitForSeconds(0.3f);
		inJump = false; 
	}

	private void GetComponents() {
		movementController = GetComponent<MovementController>();
		dropShadow = GetComponent<DropShadow>();
	}


	private IEnumerator ResetPlayerAbilites() {
		ResetMovementStates();
		yield return new WaitForSeconds(groundedDelay);
		yield return new WaitForEndOfFrame();
		SetGrounded();
		// print("isGrounded = " + isGrounded);
	}

	public void OnPlayerDeath(object source, EventArgs e) {
		ResetMovementStates();
		SetGrounded();
	}

	private void ResetMovementStates() {
		InAir = false;
		reset = true;
		inJump = false;
		movementController.IsJumping = false;
		movementController.IsStomping = false;
		movementController.DoubleJump = true;
		if(!movementController.DisableKnockback) {
			movementController.KnockedBack = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Ground")) {
			OnLanded();
		}
	}

	private void SetGrounded() {
		// OnLanded();
		movementController.Dashed = false;
		isGrounded = true;
	}

	protected virtual void OnLanded() {
		if(Landed != null) {
			Landed(this, EventArgs.Empty);
		}
	}
}
