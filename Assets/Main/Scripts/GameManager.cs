using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[SerializeField] private Player player;
	[SerializeField] private Text hpText;

	private Enemy enemy;

	public bool Goal {get; set;}

	public Player Player {
		get { return player; }
		set { player = value; }
	}

	public Enemy Enemy {
		get { return enemy; }
		set { enemy = value; }
	}

	public static GameManager Instance;


	private float timer;

	private void Awake() {
		DontDestroyOnLoad(this);
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}

	private void Update() {
		// hpText.text = "HP = " + player.HP.ToString();
		if(Goal) {
			timer += Time.deltaTime;
			if(timer > 6) {
				// SceneManager.LoadSceneAsync(0);
				// SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
			}
		}
	}
}
