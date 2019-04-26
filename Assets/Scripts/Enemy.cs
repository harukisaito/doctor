using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

	private int hp = 100;

	public override int HP {
		get { return hp; } 
		set { 
			Debug.Log("HELLO");
			hp = value;
			TakeDamage(value); 

		}
	}

	public override void TakeDamage(int damage) {
		hp -= damage;
		Die();
	}

	public override void Die() {
		if(hp <= 0) {
			Destroy(gameObject);
		}
	}



	// [SerializeField] private GameObject projectilePrefab;
	// [SerializeField] private float enemyCheckRadius;
	// [SerializeField] private float checkTime;
	// [SerializeField] private float movementSpeed;
	// [SerializeField] private LayerMask whatIsTarget;
	// [SerializeField] private Transform groundCheck;
	// [SerializeField] private float groundCheckRadius;
	// [SerializeField] private LayerMask whatIsGround;
	// private List<GameObject> projectiles = new List<GameObject>();

	// private Rigidbody2D body;
	// private Vector3 offset;
	// private bool targetInRange;
	// private bool isGrounded;
    // private int movementDirection;

    // private void Start () {
	// 	body = GetComponent<Rigidbody2D>();
	// 	StartCoroutine(CheckForTarget());
	// 	movementDirection = 1;
	// }

	// private void FixedUpdate() {
	// 	isGrounded = Physics2D.OverlapCircle(transform.localPosition, groundCheckRadius, whatIsGround);

	// 	body.velocity = new Vector2(movementSpeed * movementDirection * Time.deltaTime, body.velocity.y); 
	// }

	// private void Update() {
	// 	if(!isGrounded) {
	// 		movementDirection = movementDirection * -1;
	// 	}
	// }

	// private IEnumerator CheckForTarget() {
	// 	for(;;) {
	// 		targetInRange = Physics2D.OverlapCircle(transform.localPosition, enemyCheckRadius, whatIsTarget);
	// 		if(targetInRange) {
	// 			ShootAtTarget();
	// 		}
	// 		yield return new WaitForSeconds(checkTime);
	// 	}
	// }

	// private void ShootAtTarget() {
	// 	Vector2 playerPos = GameManager.Instance.Player.transform.localPosition;
	// 	if(playerPos.x > transform.localPosition.x) {
	// 		offset = new Vector3(0.5f, 0);
	// 	}
	// 	else if(playerPos.x < transform.localPosition.x) {
	// 		offset = new Vector3(-0.5f, 0);
	// 	}

	// 	Instantiate(projectilePrefab, transform.localPosition + offset, Quaternion.identity);
	// }
}
