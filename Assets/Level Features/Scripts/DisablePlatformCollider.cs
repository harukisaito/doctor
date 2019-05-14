using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlatformCollider : MonoBehaviour {

	[SerializeField] private Collider2D boxCollider;
	[SerializeField] private Collider2D boxColliderTrigger;


	private void Start() {
		Physics2D.IgnoreCollision(boxCollider, boxColliderTrigger);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			Physics2D.IgnoreCollision(other, boxCollider, true);
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			Physics2D.IgnoreCollision(other, boxCollider, false);
		}
	}

	public void DisableColliders() {
		boxCollider.enabled = false;
		// StartCoroutine(EnableColliders());
	}

	private IEnumerator EnableColliders() {
		yield return new WaitForSeconds(0.3f);
		boxCollider.enabled = true;
	}

}
