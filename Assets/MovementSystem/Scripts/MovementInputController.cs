using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class MovementInputController : MonoBehaviour {

	[SerializeField] private KeyCode jumpKey;
	[SerializeField] private KeyCode sprintKey;
	[SerializeField] private KeyCode floatKey;
	[SerializeField] private KeyCode duckKey;
	[SerializeField] private KeyCode dashKey;

	private float movement;
	private float movementSpeed;
	private bool isDucking;

	public bool IsDucking {
		get { return isDucking; }
		set {
			isDucking = value;
			if(isDucking == false) {
				movementController.UnDuck();
			}
		}
	}
	public bool Sprint {get; set;}
	public bool MoveDown {get; set;}


	private MovementController movementController;
	private GroundCheck groundCheck;

	private void Start() {
		movementController = GetComponent<MovementController>();
		groundCheck = GetComponent<GroundCheck>();
	}

	private void Update() {
		MovementInput();
		SprintInput();
		DuckInput();
		JumpInput();
		DashInput();
		FloatInput();
		MoveDownInput();
	}
	private void FixedUpdate() {
		if(!movementController.IsDashing) {
			if(!IsDucking) {
				movementController.Move(movementSpeed, movement, 1f, 0);
			}
		}
	}

	private void JumpInput() { 
		if(Input.GetKeyDown(jumpKey) && !IsDucking && (groundCheck.IsGrounded || movementController.DoubleJump)) {
			movementController.Jump();
		}
	}

	private void MovementInput() {
		movement = Input.GetAxis("Horizontal");
	}

	private void SprintInput() {
		Sprint = Input.GetKey(sprintKey);
		if(Sprint) {
			movementSpeed = 3f;
		}
		else {
			movementSpeed = 1.5f;
		}
	}

	private void DuckInput() {
		IsDucking = Input.GetKey(duckKey);
		if(IsDucking && groundCheck.IsGrounded) {
			movementController.Duck();
		}
	}

	private void DashInput() {
		if(!groundCheck.IsGrounded && Input.GetKeyDown(dashKey) && !IsDucking) {
			if(movementController.IsFacingRight) {
				movementController.Dash(Vector2.right);
			}
			else {
				movementController.Dash(Vector2.left);
			}
		}
	}

	private void FloatInput() {
		if(!groundCheck.IsGrounded && !IsDucking) {
			movementController.NoDownForce = Input.GetKey(floatKey);
		}
	}

	private void MoveDownInput() {
		if(groundCheck.IsGrounded) {
			if(IsDucking) {
				MoveDown = Input.GetKeyDown(jumpKey);
			}
		}
	}
}
