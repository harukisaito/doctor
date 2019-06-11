using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

	public Scenes CurrentScene {get; private set;}
	public bool ChangingScene {get; private set;}

	public static SceneManagement Instance;

	private void Awake() {
		// DontDestroyOnLoad(this);
		if(Instance == null) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}
	}

	private void Start() {
		LoadScene(Scenes.StartMenu, false);
	}

	public void LoadScene(Scenes levelIndex, bool unloadPreviousScene) {
		if(unloadPreviousScene) {
			UnLoadScene(SceneManager.GetActiveScene());
		}
		StartCoroutine(WaitForLoad(levelIndex));
	}

	public void MoveToScene(GameObject objToMove, Scenes scene) {
		SceneManager.MoveGameObjectToScene(objToMove, GetScene(scene));
	}

	private IEnumerator WaitForLoad(Scenes levelIndex) {
		ChangingScene = true;
		CurrentScene = levelIndex;

		AsyncOperation operation = SceneManager.LoadSceneAsync((int)CurrentScene, LoadSceneMode.Additive);

		// // activate loading screen
		// if(!LoadingScreenCamera.Instance.Enabled) {
		// 	LoadingScreenCamera.Instance.EnableScreen(true);
		// }

		while(!operation.isDone) {
			yield return null;
		}
		// deactivate loading screen

		// LoadingScreenCamera.Instance.EnableScreen(false);

		// yield return SceneManager.LoadSceneAsync((int)CurrentScene, LoadSceneMode.Additive);
		if(CurrentScene == Scenes.LevelSakura) {
			SpawnManager.Instance.SpawnEntities();
		}
		SceneManager.SetActiveScene(GetScene(CurrentScene));
		ChangingScene = false;
	}

	private void UnLoadScene(Scene sceneToUnLoad) {
		SceneManager.UnloadSceneAsync(sceneToUnLoad);
	}

	private Scene GetScene(Scenes sceneToGet) {
		return SceneManager.GetSceneByBuildIndex((int)sceneToGet);
	}
}
