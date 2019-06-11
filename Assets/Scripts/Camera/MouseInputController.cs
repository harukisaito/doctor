using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputController : MonoBehaviour {

	private Vector3 mousePos;

	private void Update() {
		mousePos = Input.mousePosition;
		mousePos.x =  mousePos.x / 1920 - 0.5f;
		mousePos.y =  mousePos.y / 1080 - 0.5f;
		mousePos.x *= 5;
		mousePos.y *= 5;
		mousePos.z = 0;
		transform.position = mousePos;
	}
}
