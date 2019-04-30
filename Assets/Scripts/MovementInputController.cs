using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInputController : MonoBehaviour {

	[SerializeField] private KeyCode moveLeftKey;
	[SerializeField] private KeyCode moveRightKey;
	[SerializeField] private KeyCode sprintKey;
	[SerializeField] private KeyCode floatKey;

	private float movement;
	private float movementSpeed;
	private float buttonCoolDown;
	private float buttonCoolDownTime = 0.2f;
	private int buttonCount = 0;

	private MovementController movementController;
	private GroundCheck groundCheck;

	private void Start() {
		movementController = GetComponent<MovementController>();
		groundCheck = GetComponent<GroundCheck>();
		buttonCoolDown = buttonCoolDownTime;
	}

	private void Update() {
		MovementInput();
		JumpInput();
		DashInput();
		FloatInput();
		ResetDashInput();
	}
	private void FixedUpdate() {
		if(!movementController.IsDashing) {
			if(Input.GetKey(sprintKey)) {
				movementSpeed = 2.5f;
			}
			else {
				movementSpeed = 1.5f;
			}
			movementController.Move(movementSpeed, movement);
		}
	}

	private void JumpInput() { 
		if(Input.GetKeyDown(KeyCode.Space) && (groundCheck.IsGrounded || movementController.DoubleJump)) {
			movementController.Jump();
		}
	}

	private void MovementInput() {
		movement = Input.GetAxis("Horizontal");
	}

	private void DashInput() {
		if(!groundCheck.IsGrounded && !movementController.Dashed) {
			if(Input.GetKeyDown(moveLeftKey)) {
				DoubleTapToDash(Vector2.left);
			}
			else if(Input.GetKeyDown(moveRightKey)) {
				DoubleTapToDash(Vector2.right);
			}
		}
	}

	private void FloatInput() {
		if(!groundCheck.IsGrounded) {
			movementController.NoDownForce = Input.GetKey(floatKey);
		}
	}

	private void ResetDashInput() {
		if(buttonCoolDown > 0) {
			buttonCoolDown -= 1 * Time.deltaTime;
		}
		else {
			buttonCount = 0;
		}
	}

	private void DoubleTapToDash(Vector2 direction) {
		if(buttonCoolDown > 0 && buttonCount == 1) { 
			movementController.Dash(direction);
		}
		else {
			buttonCoolDown = buttonCoolDownTime;
			buttonCount ++;
		}
	}
}
