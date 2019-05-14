using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

	public Vector3 Position {get; set;}

	private void Start() {
		Position = transform.localPosition;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			SpawnManager.Instance.CheckPoint = this;
		}
	}
}
