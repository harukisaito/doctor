using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlatformCollider : MonoBehaviour {

	[SerializeField] private Collider2D edgeCollider;
	[SerializeField] private Collider2D triggerCollider;

	private GroundCheck groundCheck;

	private float timer = 0;
	private bool fromBelowTrigger;
	private bool fromBelowSecond;

	private void Start() {
		groundCheck = GameManager.Instance.Player.GetComponent<GroundCheck>();
		Physics2D.IgnoreCollision(edgeCollider, triggerCollider, true);
		print("ON PLATFORM? = " + groundCheck.OnPlatform);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			Physics2D.IgnoreCollision(edgeCollider, other, true);
			fromBelowTrigger = true; 
			print("went through trigger, TRIGGER = " + fromBelowTrigger + " SECOND COLLIDER = " + fromBelowSecond + "ON PLATFORM = " + groundCheck.OnPlatform);
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			Physics2D.IgnoreCollision(edgeCollider, other, false);
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Player")) {
			print("collided with edge, TRIGGER = " + fromBelowTrigger + " SECOND COLLIDER = " + fromBelowSecond + "ON PLATFORM = " + groundCheck.OnPlatform);
			if(fromBelowTrigger) {
				if(fromBelowSecond) { // this is actually there to prevent collision from below and only register the second collision if from below
					groundCheck.OnPlatform = true;
					fromBelowSecond = false; // reset this when grounded on platform
					fromBelowTrigger = false;
					print("on platform, TRIGGER = " + fromBelowTrigger + " SECOND COLLIDER = " + fromBelowSecond + "ON PLATFORM = " + groundCheck.OnPlatform);
				}
				if(!groundCheck.OnPlatform) {
					fromBelowSecond = true;
					print("second one set to TRUE");
				}
			}
			else {
				groundCheck.OnPlatform = true;
				print("on platform");
			}
		}
	}

	private void OnCollisionExit2D(Collision2D other) {
		if(other.gameObject.CompareTag("Player")) {
			if(groundCheck.OnPlatform) {
				groundCheck.OnPlatform = false;
				print("exit platform, TRIGGER = " + fromBelowTrigger + " SECOND COLLIDER = " + fromBelowSecond + "ON PLATFORM = " + groundCheck.OnPlatform);
			}
		}
	}

	public void DisableColliders() {
		edgeCollider.enabled = false;
		StartCoroutine(EnableColliders());
	}

	private IEnumerator EnableColliders() {
		yield return new WaitForSeconds(0.3f);
		edgeCollider.enabled = true;
	}

}
