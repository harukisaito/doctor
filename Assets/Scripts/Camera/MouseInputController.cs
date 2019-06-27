using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputController : MonoBehaviour {

	private Vector3 mousePos;

	// private void Start() {
	// 	StartCoroutine(LerpToPosition(5f));
	// }

	// private IEnumerator LerpToPosition(float lerpTime) {
	// 	while(true) {
	// 		float time = 0;
	// 		Vector2 originalPos = transform.position;
	// 		Vector2 positionToLerpTo = GetRandomPosition();
	// 		while(time < lerpTime) {
	// 			transform.position = Vector2.Lerp(originalPos, positionToLerpTo, Time.deltaTime);
	// 			time += Time.deltaTime;
	// 			yield return null;
	// 		}
	// 	}
	// }

	private void Update() {
		mousePos = Input.mousePosition;
		mousePos.x =  mousePos.x / 1920 - 0.5f;
		mousePos.y =  mousePos.y / 1080 - 0.5f;
		mousePos.x *= 5;
		mousePos.y *= 5;
		mousePos.z = 0;
		transform.position = mousePos;
	}

	// private Vector2 GetRandomPosition() {
	// 	float randomX = Random.Range(0f, 1f) * 10f;
	// 	float randomY = Random.Range(0, 1f) * 10f;

	// 	return new Vector2(randomX, randomY);
	// }
}
