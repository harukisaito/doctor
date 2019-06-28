using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCanvas : MonoBehaviour {

	public static LevelCanvas Instance;

	private void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}
}
