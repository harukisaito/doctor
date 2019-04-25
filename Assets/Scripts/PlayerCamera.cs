using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	[SerializeField] private Transform player;

	private Vector3 offset = new Vector3(0, 0, -5);
	private float smoothTime = 0.125f;

	private void Start() {
		transform.localPosition = player.localPosition;
	}

	private void FixedUpdate() {
		// transform.LookAt(player);
		Vector3 desiredPosition = player.localPosition + offset; 
		Vector3 smoothedPosition = Vector3.Lerp(transform.localPosition, desiredPosition, smoothTime);
		transform.localPosition = smoothedPosition;
	}
}
