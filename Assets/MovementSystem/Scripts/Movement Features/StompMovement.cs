using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompMovement : MonoBehaviour {

	[SerializeField] private float stompVelocity;

	private Vector2 velocity = new Vector2(0, 0);

	public void Stomp(Rigidbody2D body) {
		velocity.y = -stompVelocity;
		body.velocity = velocity;
	}
}
