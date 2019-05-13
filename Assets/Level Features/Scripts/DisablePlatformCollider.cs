using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlatformCollider : MonoBehaviour {

	Collider2D[] colliders;

	private void Start() {
		colliders = GetComponentsInChildren<EdgeCollider2D>();
		// why the fuck does getComponentsInChildren return components of the current gameobject as well.
	}

	public void DisableColliders() {
		EnableColliders(false);
	}

	public void EnableColliders() {
		EnableColliders(true);
	}

	private void EnableColliders(bool enable) {
		for(int i = 0; i < colliders.Length - 1; i++) {
			colliders[i].enabled = enable;
		}
	}
}
