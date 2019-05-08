using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnmovableObject : MonoBehaviour {

	private Rigidbody2D body;

	private void Start () {
		body = transform.parent.GetComponent<Rigidbody2D>();
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			body.bodyType = RigidbodyType2D.Static;
		}
	}

	private void OnCollisionExit2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			body.bodyType = RigidbodyType2D.Dynamic;
		}
	}
}
