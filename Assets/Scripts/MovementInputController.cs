using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInputController : MonoBehaviour {

	[SerializeField] private KeyCode moveLeftKey;
	[SerializeField] private KeyCode moveRightKey;
	[SerializeField] private KeyCode sprintKey;

	private float movement;
	private bool sprint;
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
		JumpInput();
		SprintInput();
		DashInput();
		ResetDashInput();
	}
	private void FixedUpdate() {
		movement = Input.GetAxis("Horizontal");
		movementController.Move(sprint, movement);
	}

	private void JumpInput() { 
		if(Input.GetKeyDown(KeyCode.Space) && (groundCheck.IsGrounded || movementController.DoubleJump)) {
			movementController.Jump();
		}
	}

	private void SprintInput() {
		sprint = Input.GetKey(sprintKey);
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
			// movementController.DashForce(direction);
		}
		else {
			buttonCoolDown = buttonCoolDownTime;
			buttonCount ++;
		}
	}
}
