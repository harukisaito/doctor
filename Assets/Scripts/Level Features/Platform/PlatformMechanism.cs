using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMechanism : MonoBehaviour {

	public bool OnPlatform {get; set;}

	private float timer = 0f;

	private bool exit;

	GroundCheck groundCheck;

	// private void Start() {
	// 	groundCheck = GameManager.Instance.Player.GetComponent<GroundCheck>();
	// }

	// private void OnCollisionEnter2D(Collision2D other) {
	// 	if(other.gameObject.CompareTag("Player")) {	
	// 		// OnPlatform = true;
	// 		groundCheck.OnPlatform = true;
	// 		print("on platform");
	// 	}
	// }
	// // private void OnCollisionStay2D(Collision2D other) {
	// // 	if(other.gameObject.CompareTag("Player")) {
	// // 		timer += Time.deltaTime;
	// // 		// print(timer);
	// // 	}
	// // }
	// private void OnCollisionExit2D(Collision2D other) {
	// 	if(other.gameObject.CompareTag("Player")) {
	// 		groundCheck.OnPlatform = false;
	// 		OnPlatform = false;
	// 	}
	// }
}
