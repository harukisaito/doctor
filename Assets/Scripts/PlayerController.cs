using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float walkSpeed = 100f;
	[SerializeField] private float sprintSpeed = 180f;
	[SerializeField] private float jumpingPower = 220f;
	[SerializeField] private float dashingSpeed = 250f; 
	[SerializeField] private float checkRadius = 0.1f;	
	[SerializeField] private float dashPeriod = 0.15f;
	[SerializeField] private Transform checkTransform;
	[SerializeField] private LayerMask whatIsGround; 
	[SerializeField] private KeyCode moveLeftKey;
	[SerializeField] private KeyCode moveRightKey;
	[SerializeField] private KeyCode sprintKey;

	private Rigidbody2D body;
	private SpriteRenderer spriteRenderer;

	private float movement;
	private bool isFacingRight;
	private bool isGrounded = false;
	private bool doubleJump;
	private bool sprint;
	private bool dashed;
	private float dashTime;
	private float buttonCoolDown;
	private float buttonCoolDownTime = 0.2f;
	private int buttonCount = 0;

	private void Start() {
		body = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		isFacingRight = true;
		dashTime = dashPeriod;
		buttonCoolDown = buttonCoolDownTime;
	}

	private void Update() {

		ResetOnGround();
		ResetDash();

		if(Input.GetKeyDown(KeyCode.Space) && (isGrounded || doubleJump)) {
			Jump();
		}
		
		sprint = Input.GetKey(sprintKey);

		if(movement > 0 && !isFacingRight) {
			FlipSprite();
		}
		else if(movement < 0 && isFacingRight) {
			FlipSprite();
		}

		if(!isGrounded && !dashed) {
			if(Input.GetKeyDown(moveLeftKey)) {
				DoubleTapToDash(Vector2.left);
			}
			else if(Input.GetKeyDown(moveRightKey)) {
				DoubleTapToDash(Vector2.right);
			}
		}
	}

	private void FixedUpdate() {

		isGrounded = Physics2D.OverlapCircle(checkTransform.position, checkRadius, whatIsGround);

		if(dashTime == dashPeriod) {
			movement = Input.GetAxis("Horizontal");
			if(!sprint) {
				body.velocity = new Vector2(movement * walkSpeed * Time.deltaTime, body.velocity.y);
			} 
			else if(sprint) {
				body.velocity = new Vector2(movement * sprintSpeed * Time.deltaTime, body.velocity.y);
			}
		}
	}

	private void FlipSprite() {
		isFacingRight = !isFacingRight;
		spriteRenderer.flipX = !isFacingRight;
	}


	private void DoubleTapToDash(Vector2 direction) {
		if(buttonCoolDown > 0 && buttonCount == 1) { 
			StartCoroutine(Dash(direction));
		}
		else {
			buttonCoolDown = buttonCoolDownTime;
			buttonCount ++;
		}
	}
	private IEnumerator Dash(Vector2 direction) {
		dashed = true;

		while(dashTime >= 0) {
			float time = Time.deltaTime;

			dashTime -= time;
			body.velocity = direction * dashingSpeed * time;	
			yield return null;
		}
		dashTime = dashPeriod;
	}

	private void Jump() {
		body.AddForce(Vector2.up * jumpingPower);
		if(doubleJump && !isGrounded) {
			doubleJump = false;
		}
	}

	private void ResetDash() {
		if(buttonCoolDown > 0) {
			buttonCoolDown -= 1 * Time.deltaTime;
		}
		else {
			buttonCount = 0;
		}
	}

	private void ResetOnGround() {
		if(isGrounded) {
			doubleJump = true;
			dashed = false;
		}
	}
}
