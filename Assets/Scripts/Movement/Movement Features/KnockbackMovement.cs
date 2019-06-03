using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackMovement : MonoBehaviour {

	private Vector2 knockback;

	public void Knockback(KnockbackDirection direction, Rigidbody2D body, float forceAmount, float knockbackHeight) {
		knockback = new Vector2((int)direction, knockbackHeight);
		body.velocity = Vector2.zero;
		body.AddForce(knockback * forceAmount, ForceMode2D.Impulse);
	}
}
