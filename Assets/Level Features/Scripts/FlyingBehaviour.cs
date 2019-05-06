using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBehaviour : MonoBehaviour {

	[SerializeField] private bool moveRight;
	[SerializeField] private float speed;

	public bool OnPlatform {get; set;}

	public bool MoveRight {
		get { return moveRight; }
	}

	public float Speed {
		get { return speed; }
	}

	private void FixedUpdate () {
		if(moveRight) {
			transform.localPosition += Vector3.right * speed * Time.deltaTime;
		}
		else {
			transform.localPosition += Vector3.left * speed * Time.deltaTime;
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			other.transform.parent = transform;
		}
	}

	private void OnCollisionExit2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			other.transform.parent = null;
		}
	}
}
