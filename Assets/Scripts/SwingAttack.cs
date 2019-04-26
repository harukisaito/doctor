using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAttack : MonoBehaviour {

	[SerializeField] private KeyCode attackButton;
	[SerializeField] private float attackTime;

	private Collider2D circleCollider;

	private void Start() {
		circleCollider = GetComponent<Collider2D>();
		circleCollider.enabled = false;
	}

	private void Update() {
		if(Input.GetKeyDown(attackButton)) {
			Debug.Log("HELLo");
			circleCollider.enabled = true;
			StartCoroutine(DeactivateCollider());
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		Debug.Log(other.gameObject.name);
		other.GetComponent<Enemy>().TakeDamage(100);
	}

	private IEnumerator DeactivateCollider() {
		yield return new WaitForSeconds(attackTime);
		circleCollider.enabled = false;
	}
}
