using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	private float timer;

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			GameManager.Instance.Goal = true;
		}
	}

	private void FixedUpdate() {
		if(GameManager.Instance.Goal && timer < 6) {
			transform.localPosition += Vector3.up * Time.deltaTime;
			timer += Time.deltaTime;
		}
	}
}
