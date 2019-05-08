using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBehaviour : MonoBehaviour {


	private FlyingObject flyingObject;

	private void Start() {
		flyingObject = GetComponent<FlyingObject>();
		transform.localScale = new Vector3(flyingObject.Length, flyingObject.Height, 0);
		// moveRight = flyingObject.MovingRight;
		// speed = flyingObject.Speed;
	}

	// private void FixedUpdate () {
	// 	if(moveRight) {
	// 		transform.localPosition += Vector3.right * speed * Time.deltaTime;
	// 	}
	// 	else {
	// 		transform.localPosition += Vector3.left * speed * Time.deltaTime;
	// 	}
	// }

	// private void OnCollisionEnter2D(Collision2D other) {
	// 	if(other.gameObject.tag == "Player") {
	// 		other.transform.parent = transform;
	// 	}
	// }

	// private void OnCollisionExit2D(Collision2D other) {
	// 	if(other.gameObject.tag == "Player") {
	// 		other.transform.parent = null;
	// 	}
	// }
}
