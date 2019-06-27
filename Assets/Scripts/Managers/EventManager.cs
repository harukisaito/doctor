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
	private PlayerParticles playerParticles;
	private Goal goal;
	private Attack[] attacks;

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
		SceneManagement.Instance.FinishedLoadingLevel += GameManager.Instance.OnFinishedLoadingLevel;
		SceneManagement.Instance.FinishedLoadingLevel += ManagerCamera.Instance.OnFinishedLoadingLevel;
		SceneManagement.Instance.FinishedLoadingLevel += UIManager.Instance.OnFinishedLoadingLevel;

		SceneManagement.Instance.FinishedLoadingEndMenu += UIManager.Instance.OnFinishedLoadingEndMenu;
		SceneManagement.Instance.FinishedLoadingStartMenu += ManagerCamera.Instance.OnFinishedLoadingStartMenu;
		SceneManagement.Instance.FinishedLoadingEndMenu += ManagerCamera.Instance.OnFinishedLoadingEndMenu;
	}

	private void OnFinishedLoadingLevel(object source, EventArgs e) {
		GetReferences();

		player.PlayerDeath += movementController.OnPlayerDeath;
		player.PlayerDeath += groundCheck.OnPlayerDeath;
		player.PlayerDeath += trailRender.OnPlayerDeath;
		player.PlayerDeath += UIManager.Instance.OnPlayerDeath;

		foreach(var a in attacks) {
			player.PlayerDeath += a.OnPlayerDeath;
		}

		player.PlayerDamage += UIManager.Instance.OnPlayerDamage;

		groundCheck.Landed += playerParticles.OnLanded;

		dashMovement.DashStart += dashAttack.OnDashStart;
		dashMovement.DashEnd += dashAttack.OnDashEnd;
		dashMovement.DashStart += player.OnDashStart;
		dashMovement.DashEnd += player.OnDashEnd;
		dashMovement.DashStart += trailRender.OnDashStart;
		dashMovement.DashEnd += trailRender.OnDashEnd;

		goal.Finish += SpawnManager.Instance.OnFinish;
		goal.Finish += ObjectPoolManager.Instance.OnFinish;
		// goal.Finish += ObjectPoolManager.Instance.OnFinishedLoadingLevel;
		goal.Finish += GameManager.Instance.OnFinish;

		goal.FinishAnimation += ParticleManager.Instance.OnFinishAnimation;
		goal.FinishAnimation += ManagerCamera.Instance.OnFinishAnimation;
		goal.FinishAnimation += playerParticles.OnFinishAnimation;
	}

	private void GetReferences() {
		player = GameManager.Instance.Player;
		goal = Goal.Instance;
		movementController = player.GetComponent<MovementController>();
		groundCheck = player.GetComponent<GroundCheck>();
		dashMovement = player.GetComponent<DashMovement>();
		dashAttack = player.GetComponentInChildren<DashAttack>();
		trailRender = player.GetComponentInChildren<TrailRender>();
		playerParticles = player.GetComponent<PlayerParticles>();
		cameraShake = PlayerCamera.Instance.GetComponentInChildren<CameraShake>();
		attacks = player.GetComponentsInChildren<Attack>();
	}
}
