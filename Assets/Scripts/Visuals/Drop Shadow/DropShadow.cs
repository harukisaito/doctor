using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropShadow : MonoBehaviour {

	private SpriteRenderer[] spriteRenderers;
	private SpriteRenderer dropShadow;

	public bool EnableDropShadow {
		set {
			if(value == true) {
				dropShadow.enabled = value;
			}
			else dropShadow.enabled = value;
		}
	}

	private void Start() {
		spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		foreach(var sR in spriteRenderers) {
			if(sR.gameObject.CompareTag("Drop Shadow")) {
				dropShadow = sR;
				dropShadow.enabled = false;
			}
		}
	}
}
