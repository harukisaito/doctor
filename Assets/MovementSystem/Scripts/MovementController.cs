using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundCheck))]
public class MovementController : MonoBehaviour {

	[SerializeField] private float speed = 100f;
	[SerializeField] private float jumpingPower = 220f;
	[SerializeField] private float downForce;
	[SerializeField] private float floatiness;

	private Rigidbody2D body;
	private SpriteRenderer spriteRenderer;
	private GroundCheck groundCheck;
	private DashMovement dashMovement;
	private DuckMovement duckMovement;

	private bool doubleJump;
	private bool dashed;
	private bool isFacingRight;
	private float movementDirection;

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

	public bool IsFacingRight {
		get { return isFacingRight; }
		set { isFacingRight = value; }
	}

	public float MovementDirection {
		get { return movementDirection; }
	}

	public Rigidbody2D Player {
		get { return body; }
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
		if(body.velocity.y < 0 && !groundCheck.IsGrounded) {
			if(!NoDownForce) {
				body.velocity += Vector2.down * downForce / body.velocity.y * body.velocity.y * Time.deltaTime;
			}
			else if(NoDownForce) {
				body.velocity = Vector2.down * downForce * floatiness * Time.deltaTime;
			}
		}
	}

	public void Move(float movementSpeedMultiplier, float movementDirection) {
		if(IsDucking) {
			UnDuck();
		}
		if(body == null) {
			body = GetComponent<Rigidbody2D>();
		}

		this.movementDirection = movementDirection;
		float movementSpeed = speed;
		movementSpeed *= movementSpeedMultiplier;

		body.velocity = new Vector2(movementDirection * movementSpeed * Time.deltaTime, body.velocity.y);

		FlipSprite();
	}

	public void Duck() {
		duckMovement.Duck(body);
		IsDucking = true;
	}

	private void UnDuck() {
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
