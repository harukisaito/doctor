using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeText : MonoBehaviour {

	public static TimeText Instance;

	private void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}
}
