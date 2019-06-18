using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour {

	[SerializeField] private float speedX = 100f;
	[SerializeField] private float speedY = 100f;
	[SerializeField] private float jumpingPower = 220f;
	[SerializeField] private float downForce = 8f;
	[SerializeField] private float floatiness;

	private Rigidbody2D body;
	private SpriteRenderer spriteRenderer;
	private GroundCheck groundCheck;
	private DashMovement dashMovement;
	private DuckMovement duckMovement;
	private KnockbackMovement knockbackMovement;
	private StompMovement stompMovement;

	private bool firstTime = true;
	private Vector2 down = Vector2.down;

	public bool DoubleJump {get; set;}
	public bool Dashed {get; set;}
	public bool DownForce {get; set;}
	public bool KnockedBack {get; set;}
	public bool DisableKnockback {get; set;}
	public bool Stop {get; set;}
	public bool IsJumping {get; set;}
	public bool IsDashing {get; set;}
	public bool IsDucking {get; private set;}
	public bool IsStomping {get; set;}
	public bool IsFacingRight {get; private set;}

	public bool MovingDown {get; set;}

	public float MovementDirection {get; private set;}
	public Vector2 StartingVelocity {get; set;}
	public Vector2 Velocity {get; private set;}

	public EventHandler Jumped;

	private void Awake() {
		GetComponents(); 
	}

	private void Start() {
		IsFacingRight = true;
		DownForce = true;
	}

	private void FixedUpdate() {
		if(groundCheck != null) {
			float deltaTime = Time.deltaTime;
			float yVelocity = body.velocity.y;

			if(yVelocity < 0 && !groundCheck.IsGrounded) {
				if(DownForce) {
					body.velocity += down * downForce * deltaTime;
				}
				else if(!DownForce) {
					body.velocity = down * downForce * floatiness * deltaTime;
				}
			}
			else if(yVelocity > 0 && !groundCheck.IsGrounded) {
				body.velocity += down * downForce * deltaTime;
			}
		}
	}

	public void Move(
		float movementSpeedMultiplierX, float movementDirectionX, 
		float movementSpeedMultiplierY, float movementDirectionY) {

		MovementDirection = movementDirectionX;

		float movementSpeedX = speedX;
		float movementSpeedY = speedY;

		movementSpeedX *= movementSpeedMultiplierX;
		movementSpeedY *= movementSpeedMultiplierY;

		Velocity = new Vector2(
			x: movementDirectionX * movementSpeedX * Time.deltaTime, 
			y: movementDirectionY == 0 ? body.velocity.y : movementDirectionY * movementSpeedY * Time.deltaTime
		);

		SetStartingVelocity();

		if(!Stop) {
			body.velocity = Velocity;
		}

		FlipSprite();
	}

	private void SetStartingVelocity() {
		if(firstTime) {
			StartingVelocity = Velocity;
			firstTime = false;
		}
	}

	public void Duck() {
		duckMovement.Duck(body);
		IsDucking = true;
	}

	public void UnDuck() {
		duckMovement.UnDuck(body);
		IsDucking = false;
	}

	public void Stomp() {
		IsStomping = true;
		stompMovement.Stomp(body);
	}

	private void FlipSprite() {
		if(MovementDirection > 0 && !IsFacingRight) {
			Flip();
		}
		else if(MovementDirection < 0 && IsFacingRight) {
			Flip();
		}
	}

	private void Flip() {
		IsFacingRight = !IsFacingRight;
		spriteRenderer.flipX = !IsFacingRight;
	}

	public void Jump(float jumpForce = 1f) {
		OnJumped();
		groundCheck.CanJump = false;
		IsJumping = true;
		AddVelocity(jumpForce);
	}

	public void MoveDown() {
		// AddVelocity(-0.035f);
		MovingDown = true;
		StartCoroutine(ResetMovingDown());
	}

	private IEnumerator ResetMovingDown() {
		yield return null;
		MovingDown = false;
	}

	private void AddVelocity(float direction) {
		if(DoubleJump && !groundCheck.IsGrounded) {
			DoubleJump = false;	
			body.velocity = Vector2.zero;
		}
		Vector2 velocity = body.velocity;
		velocity.y += jumpingPower * direction;
		body.velocity = velocity;
	}

	public void Dash(Vector2 direction) {
		if(!IsDashing) {
			dashMovement.Dash(direction, body);
			Dashed = true;
		}
	}

	public void Knockback(KnockbackDirection direction, float forceAmount, float knockbackHeight) {
		KnockedBack = true;
		DisableKnockback = true;
		knockbackMovement.Knockback(direction, body, forceAmount, knockbackHeight);
		StartCoroutine(Disable());
	}

	private IEnumerator Disable() {
		yield return new WaitForSeconds(0.1f);
		DisableKnockback = false;
		yield return new WaitForSeconds(1f);
		KnockedBack = false;
	}

	private void GetComponents() {
		body = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		groundCheck = GetComponent<GroundCheck>();
		dashMovement = GetComponent<DashMovement>();
		duckMovement = GetComponent<DuckMovement>();
		knockbackMovement = GetComponent<KnockbackMovement>();
		stompMovement = GetComponent<StompMovement>();
	}

	public void OnPlayerDeath(object source, EventArgs e) {
		IsJumping = false;
		IsDashing = false;
		IsDucking = false;
		IsStomping = false;
		spriteRenderer.enabled = true;
		dashMovement.ResetColliderOrientation();
	}

	protected virtual void OnJumped() {
		if(Jumped != null) {
			Jumped(this, EventArgs.Empty);
		}
	}
}
