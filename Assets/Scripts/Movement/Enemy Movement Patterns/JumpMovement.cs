using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMovement : MonoBehaviour {

	[SerializeField] private float jumpCoolDown;

	private MovementController movementController;

	private void Start() {
		movementController = GetComponent<MovementController>();
		StartCoroutine(Jump());
	}

	private IEnumerator Jump() {
		while(true) {
			movementController.Jump();
			yield return new WaitForSeconds(jumpCoolDown);
		}
	}
}
