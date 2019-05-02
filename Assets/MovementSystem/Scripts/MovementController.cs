using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundCheck))]
public class MovementController : MonoBehaviour {

	[SerializeField] private float speed = 100f;
	[SerializeField] private float jumpingPower = 220f;
	[SerializeField] private float dashingSpeed = 250f;
	[SerializeField] private float dashPeriod = 0.15f;
	[SerializeField] private float downForce;
	[SerializeField] private float floatiness;

	private Rigidbody2D body;
	private SpriteRenderer spriteRenderer;
	private GroundCheck groundCheck;

	private bool doubleJump;
	private bool dashed;
	private bool isFacingRight;
	private float dashTime;
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
		dashTime = dashPeriod;
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
		this.movementDirection = movementDirection;
		float movementSpeed = speed;
		movementSpeed *= movementSpeedMultiplier;
		if(body == null) {
			body = GetComponent<Rigidbody2D>();
		}
		body.velocity = new Vector2(movementDirection * movementSpeed * Time.deltaTime, body.velocity.y);

		if(movementDirection > 0 && !isFacingRight) {
			FlipSprite();
		}
		else if(movementDirection < 0 && isFacingRight) {
			FlipSprite();
		}
	}

	private void FlipSprite() {
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
		StartCoroutine(DashCoroutine(direction));
	}

	private IEnumerator DashCoroutine(Vector2 direction) {	
		IsDashing = true;
		dashed = true;

		while(dashTime >= 0) {
			float time = Time.deltaTime;

			dashTime -= time;
			body.velocity = direction * dashingSpeed * time;	
			yield return new WaitForFixedUpdate();
		}
		body.velocity = Vector2.zero;
		dashTime = dashPeriod;
		IsDashing = false;
	}
}
