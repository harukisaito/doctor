using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	private Player player;
	private MovementController movementController;
	private GroundCheck groundCheck;
	private DashMovement dashMovement;
	private DashAttack dashAttack;
	private TrailRender trailRender;
	private CameraShake cameraShake;

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
		groundCheck = player.GetComponent<GroundCheck>();
		dashMovement = player.GetComponent<DashMovement>();
		dashAttack = player.GetComponentInChildren<DashAttack>();
		trailRender = player.GetComponentInChildren<TrailRender>();
		cameraShake = PlayerCamera.Instance.GetComponentInChildren<CameraShake>();

		player.PlayerDeath += movementController.OnPlayerDeath;
		player.PlayerDeath += groundCheck.OnPlayerDeath;
		player.PlayerDeath += trailRender.OnPlayerDeath;

		dashMovement.DashStart += dashAttack.OnDashStart;
		dashMovement.DashEnd += dashAttack.OnDashEnd;
		dashMovement.DashStart += player.OnDashStart;
		dashMovement.DashEnd += player.OnDashEnd;
		dashMovement.DashStart += trailRender.OnDashStart;
		dashMovement.DashEnd += trailRender.OnDashEnd;
		// dashMovement.DashStart += cameraShake.OnDashStart;
	}
}
