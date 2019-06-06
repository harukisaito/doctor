using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlatformCollider : MonoBehaviour {


	private PlatformEffector2D platform;

	private void Start() {
		platform = GetComponent<PlatformEffector2D>();
	}

	public void AllowMovingDown() {
		platform.rotationalOffset = 180;
		StartCoroutine(ResetPlatform());
	}

	private IEnumerator ResetPlatform() {
		yield return new WaitForSeconds(0.3f);
		platform.rotationalOffset = 0;
	}

}
