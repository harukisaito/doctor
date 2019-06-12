using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private Player player;

	public bool Goal {get; set;}

	public Player Player {
		get { return player; }
		set { player = value; }
	}

	public static GameManager Instance;


	private float timer;

	private void Awake() {
		// DontDestroyOnLoad(this);
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
			}
		}
	}
}
