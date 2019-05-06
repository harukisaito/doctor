using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	[SerializeField] private float offset = 5;

	private Vector3 offsetVec;

	private float smoothTime = 0.125f;
	private bool visited;

	private void Start() {
		offsetVec = new Vector3(0, 0, -offset);
		transform.position = GameManager.Instance.Player.transform.position;
	}

	private void FixedUpdate() {
		if(GameManager.Instance.Player != null) {
			Vector3 desiredPosition = GameManager.Instance.Player.transform.position + offsetVec; 
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothTime);
			transform.position = smoothedPosition;
		}
	}
}
