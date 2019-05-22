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
	[SerializeField] private KeyCode attackKey;

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
	private AttackController attackController;
	private GroundCheck groundCheck;

	private void Start() {
		movementController = GetComponent<MovementController>();
		attackController = GetComponent<AttackController>();
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
		AttackInput();
	}
	
	private void FixedUpdate() {
		if(!movementController.IsDashing) {
			if(!IsDucking && !movementController.KnockBacked) {
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
		if(!groundCheck.IsGrounded && Input.GetKeyDown(dashKey) && !IsDucking && !movementController.Dashed) {
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

	private void AttackInput() {
		if(Input.GetKeyDown(attackKey)) {
			if(groundCheck.IsGrounded) {
				attackController.Attack(AttackPattern.GroundAttack);
			}
			else {
				attackController.Attack(AttackPattern.AirAttack);
			}
		}
	}
}
