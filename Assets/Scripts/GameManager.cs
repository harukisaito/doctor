using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[SerializeField] private GameObject player;
	[SerializeField] private GameObject buddy;

	public GameObject Player {
		get { return player; }
		// set { player = value; }
	}
	public GameObject Buddy {
		get { return buddy; }
		// set { player = value; }
	}

	public static GameManager Instance;

	private void Start() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(this);
		}
	}
}
