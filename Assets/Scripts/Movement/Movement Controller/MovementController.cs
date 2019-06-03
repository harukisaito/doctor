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

	private bool doubleJump;
	private bool dashed;
	private bool isFacingRight;
	private float movementDirection;
	private bool firstTime = true;
	private Vector2 velocity;
	private Vector2 down = Vector2.down;

	public bool DoubleJump {
		get { return doubleJump; }
		set { doubleJump = value; }
	}

	public bool Dashed {
		get { return dashed; }
		set { dashed = value; }
	}

	public bool IsDashing {get; set;}
	public bool NoDownForce {get; set;}
	public bool IsDucking {get; set;}
	public bool KnockedBack {get; set;}
	public bool DisableKnockback {get; set;}
	public bool IsStomping {get; set;}
	public bool Stop {get; set;}
	public Vector2 StartingVelocity {get; set;}

	public bool IsFacingRight {
		get { return isFacingRight; }
		set { isFacingRight = value; }
	}

	public float MovementDirection {
		get { return movementDirection; }
	}

	public Vector2 Velocity {
		get { return velocity; }
	}

	private void Awake() {
		GetComponents(); 
	}

	private void Start() {
		isFacingRight = true;
	}

	private void FixedUpdate() {
		if(groundCheck != null) {
			float deltaTime = Time.deltaTime;
			float yVelocity = body.velocity.y;
			if(gameObject.tag == "Player") {
				// print(groundCheck.IsGrounded);
			}
			if(yVelocity < 0 && !groundCheck.IsGrounded) {
				if(!NoDownForce) {
					body.velocity += down * downForce * deltaTime;
				}
				else if(NoDownForce) {
					body.velocity = down * downForce * floatiness * deltaTime;
				}
			}
			else if(body.velocity.y > 0 && !groundCheck.IsGrounded) {
				body.velocity += down * downForce * deltaTime;
				if(gameObject.tag == "Player") {
					// print("downFOrce");
				}
			}
		}
	}

	public void Move(
		float movementSpeedMultiplierX, float movementDirectionX, 
		float movementSpeedMultiplierY, float movementDirectionY) {

		movementDirection = movementDirectionX;

		float movementSpeedX = speedX;
		float movementSpeedY = speedY;

		movementSpeedX *= movementSpeedMultiplierX;
		movementSpeedY *= movementSpeedMultiplierY;

		velocity = new Vector2(
			x: movementDirectionX * movementSpeedX * Time.deltaTime, 
			y: movementDirectionY == 0 ? body.velocity.y : movementDirectionY * movementSpeedY * Time.deltaTime
		);

		SetStartingVelocity();

		if(!Stop) {
			body.velocity = velocity;
		}

		FlipSprite();
	}

	private void SetStartingVelocity() {
		if(firstTime) {
			StartingVelocity = velocity;
			firstTime = false;
		}
	}

	// public void AddVelocity(Vector2 velocity) {
	// 	if(body.velocity.x < 10) {
	// 		body.velocity += velocity * platformConst;
	// 	}
	// }

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
		if(movementDirection > 0 && !isFacingRight) {
			Flip();
		}
		else if(movementDirection < 0 && isFacingRight) {
			Flip();
		}
	}

	private void Flip() {
		isFacingRight = !isFacingRight;
		spriteRenderer.flipX = !isFacingRight;
	}

	public void Jump(float jumpForce = 1f) {
		AddVelocity(jumpForce);
	}

	public void MoveDown() {
		AddVelocity(-0.035f);
	}

	private void AddVelocity(float direction) {
		if(doubleJump && !groundCheck.IsGrounded) {
			doubleJump = false;	
			body.velocity = Vector2.zero;
		}
		Vector2 velocity = body.velocity;
		velocity.y += jumpingPower * direction;
		body.velocity = velocity;
	}

	public void Dash(Vector2 direction) {
		if(!IsDashing) {
			dashMovement.Dash(direction, body);
			dashed = true;
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
}
