using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField] private Transform target;

	private Vector2 desiredPos;

	private void Start() {
		desiredPos = target.localPosition;
		Debug.Log(desiredPos);
	}

	private void Update() {
		transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPos, 0.5f);
		if(transform.localPosition.x > 50 || transform.localPosition.y > 50) {
			Destroy(gameObject);
		}
	}
}
