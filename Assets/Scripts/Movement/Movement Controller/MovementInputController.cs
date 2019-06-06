using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class MovementInputController : MonoBehaviour {

	[SerializeField] private KeyCode moveLeftKey;
	[SerializeField] private KeyCode moveRightKey;

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
		if(
			!IsDucking &&
			!movementController.IsDashing &&
			!movementController.KnockedBack &&
			!movementController.IsStomping) {
				movementController.Move(movementSpeed, movement, 1f, 0);
		}
	}

	private void JumpInput() { 
		if(Input.GetKeyDown(jumpKey) && !IsDucking && !movementController.IsDashing) {
			if(groundCheck.CanJump || movementController.DoubleJump) { 
				movementController.Jump();
			}
		}
	}

	private void MovementInput() {
		float move = Input.GetAxis("Horizontal");
		if(move < -0) {
			movement = -1;
		} else
		if(move > 0) {
			movement = 1;
		} else 
		if (move == 0) {
			movement = 0;
		}
	}

	private void SprintInput() {
		if(groundCheck.IsGrounded) {
			Sprint = Input.GetKey(sprintKey);
			if(Sprint) {
				movementSpeed = 3f;
			}
			else {
				movementSpeed = 1.5f;
			}
		}
	}

	private void DuckInput() {
		IsDucking = Input.GetKey(duckKey);
		if(IsDucking) {
			if(groundCheck.IsGrounded) {
				movementController.Duck();
			}
			else {
				movementController.Stomp();
			}
		}
	}

	private void DashInput() {
		if(Input.GetKeyDown(dashKey) && !IsDucking && !movementController.Dashed) {
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
			movementController.DownForce = !Input.GetKey(floatKey);
		}
	}

	private void MoveDownInput() {
		if(groundCheck.IsGrounded) {
			if(IsDucking) {
				if(Input.GetKeyDown(jumpKey)) {
					movementController.MoveDown();
				}
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
