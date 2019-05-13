using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnabler : MonoBehaviour {

	private DisablePlatformCollider disabler;


	public bool InCollider {get; set;}
	public bool EnteredCollider {get; set;}

	private void Start() {
		disabler = GetComponentInParent<DisablePlatformCollider>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			EnteredCollider = true;
		}
	}

	private void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			if(EnteredCollider) {
				InCollider = true;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			InCollider = false;
			EnteredCollider = false;
			disabler.EnableColliders();
		}
	}
}
