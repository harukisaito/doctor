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

	private Rigidbody2D body;
	private SpriteRenderer spriteRenderer;
	private GroundCheck groundCheck;

	private bool doubleJump;
	private bool dashed;
	private bool isFacingRight;

	private float dashTime;

	public bool DoubleJump {
		get { return doubleJump; }
		set { doubleJump = value; }
	}

	public bool Dashed {
		get { return dashed; }
		set { dashed = value; }
	}

	public bool IsFacingRight {
		get { return IsFacingRight; }
		set { isFacingRight = value; }
	}

	private void Start() {
		body = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		groundCheck = GetComponent<GroundCheck>();
		dashTime = dashPeriod;
		isFacingRight = true;
	}

	public void Move(bool sprint, float movementDirection) {
		if(sprint) {
			speed = 200f;
		}
		else {
			speed = 100f;
		}
		body.velocity = new Vector2(movementDirection * speed * Time.deltaTime, body.velocity.y);

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
		body.AddForce(Vector2.up * jumpingPower);
		if(doubleJump && !groundCheck.IsGrounded) {
			doubleJump = false;
		}
	}

	public void Dash(Vector2 direction) {
		StartCoroutine(DashCoroutine(direction));
	}

	public IEnumerator DashCoroutine(Vector2 direction) {	
		dashed = true;

		while(dashTime >= 0) {
			Debug.Log("DASH");	
			float time = Time.deltaTime;
			dashTime -= time;
			body.velocity = direction * dashingSpeed * time;	
			yield return null;
		}
		dashTime = dashPeriod;
	}

	// public void DashForce(Vector2 direction) {
	// 	dashed = true;
	// 	body.AddForce(direction * dashingSpeed * Time.deltaTime);
	// }
}
