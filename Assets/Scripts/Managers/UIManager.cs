using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public static UIManager Instance;

	private void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}

	public void StartLevel() {
		SceneManagement.Instance.LoadScene(Scenes.LevelSakura, true);
	}

	public void Quit() {
		Application.Quit();
	}
}
