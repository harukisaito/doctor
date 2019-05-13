using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnmovableObject : MonoBehaviour {

	private DisablePlatformCollider disablePlatform;

	private void Start() {
		disablePlatform = GetComponentInParent<DisablePlatformCollider>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player") {
			disablePlatform.DisableColliders();
		}
	}
}
