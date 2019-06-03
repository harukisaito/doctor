using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

	public static SceneManagement Instance;

	private void Awake() {
		DontDestroyOnLoad(this);
		if(Instance == null) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}
	}

	private void Start() {
		// LoadScene(Scenes.Introduction);
	}

	private void LoadScene(Scenes levelIndex) {
		SceneManager.LoadSceneAsync((int)levelIndex);
	}
}
