using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidStance : MonoBehaviour {

	private Vector3 originalPosition;

	private void Start() {
		originalPosition = transform.localPosition;
	}

	private void FixedUpdate() {
		if(transform.localPosition != originalPosition) {
			transform.localPosition = Vector3.Lerp(transform.position, originalPosition, 0.1f);
			// print("going back");
		}
	}
}
