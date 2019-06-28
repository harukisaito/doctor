using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBehaviour : MonoBehaviour {

	[SerializeField] private float frequency;
	[SerializeField] private float amplitude;

	private float angle;
	private float startPosY;

	private void Awake() {
		startPosY = transform.localPosition.y;
	}

	private void Update() {
		angle += Time.deltaTime * frequency;
		if(angle > 360) {
			angle -= 360;
		}
		float posY = Mathf.Sin(angle);
		posY *= amplitude;
		transform.localPosition = new Vector2(transform.localPosition.x, posY + startPosY);
	}
}
