using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	private Player player;
	private MovementController movementController;
	private TrailEffect trail;

	public static EventManager Instance;
	private void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}

	private void Start() {
		SceneManagement.Instance.FinishedLoadingLevel += SpawnManager.Instance.OnFinishedLoadingLevel;
		SceneManagement.Instance.FinishedLoadingLevel += OnFinishedLoadingLevel;
	}

	private void OnFinishedLoadingLevel(object source, EventArgs e) {
		player = GameManager.Instance.Player;
		movementController = player.GetComponent<MovementController>();
		trail = player.GetComponent<TrailEffect>();

		player.PlayerDeath += movementController.OnPlayerDeath;
		// player.PlayerDeath += trail.OnPlayerDeath;
	}
}
