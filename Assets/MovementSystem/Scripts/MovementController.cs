using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour {

	[SerializeField] private float speedX = 100f;
	[SerializeField] private float speedY = 100f;
	[SerializeField] private float jumpingPower = 220f;
	[SerializeField] private float downForce;
	[SerializeField] private float floatiness;

	private Rigidbody2D body;
	private SpriteRenderer spriteRenderer;
	private GroundCheck groundCheck;
	private DashMovement dashMovement;
	private DuckMovement duckMovement;

	private const float platformConst = 0.77f;

	private bool doubleJump;
	private bool dashed;
	private bool isFacingRight;
	private float movementDirection;
	private bool firstTime = true;
	private Vector2 velocity;

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

	private void Start() {
		body = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		groundCheck = GetComponent<GroundCheck>();
		dashMovement = GetComponent<DashMovement>();
		duckMovement = GetComponent<DuckMovement>();
		isFacingRight = true;
	}

	private void FixedUpdate() {
		if(groundCheck != null) {
			if(body.velocity.y < 0 && !groundCheck.IsGrounded) {
				if(!NoDownForce) {
					body.velocity += Vector2.down * downForce / body.velocity.y * body.velocity.y * Time.deltaTime;
				}
				else if(NoDownForce) {
					body.velocity = Vector2.down * downForce * floatiness * Time.deltaTime;
				}
			}
		}
	}

	// private void Update() {
	// 	if(gameObject.tag == "Player") {
	// 		Debug.Log(body.velocity);
	// 	}
	// }

	public void Move(float movementSpeedMultiplierX, float movementDirectionX, float movementSpeedMultiplierY, float movementDirectionY) {

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

		body.velocity = velocity;

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

	public void Jump() {
		if(doubleJump && !groundCheck.IsGrounded) {
			doubleJump = false;	
			body.velocity = Vector2.zero;
		}
		body.AddForce(Vector2.up * jumpingPower);
	}

	public void Dash(Vector2 direction) {
		dashMovement.Dash(direction, body);
		dashed = true;
	}
}
