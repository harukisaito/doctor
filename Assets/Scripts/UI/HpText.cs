using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpText : MonoBehaviour {

	public static HpText Instance;

	private void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}
}
