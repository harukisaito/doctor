using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	[SerializeField] private bool spawnEnemies = true;
	[SerializeField] private Transform playerSpawn;

	[SerializeField] private Transform[] leftRightSpawns, leftRightShootSpawns, upDownSpawns, waveSpawns, zigZagSpawns, shootSpawns, jumpSpawns;

	private List<Transform[]> enemySpawns = new List<Transform[]>();
	private EntitySpawner spawner;
	private CheckPoint checkPoint;

	public bool Spawned {get; private set;}

	public EntitySpawner Spawner {
		get { return spawner; }
	}

	public CheckPoint CheckPoint {
		get { return checkPoint; }
		set { checkPoint = value; }
	}

	public static SpawnManager Instance;

	private void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}

	private void Start() {
		spawner = GetComponent<EntitySpawner>();
		AddEnemySpawns();
	}

	public void OnFinishedLoadingLevel(object source, EventArgs e) {
		SpawnEntities();
	}

	public void SpawnEntities() {
		SpawnPlayer(playerSpawn.position);
		if(spawnEnemies) {
			SpawnAllEnemies();
		}
		PlayerCamera.Instance.Initialize();
		Spawned = true;
	}

	private void AddEnemySpawns() {
		enemySpawns.Add(leftRightSpawns);
		enemySpawns.Add(leftRightShootSpawns);
		enemySpawns.Add(upDownSpawns);
		enemySpawns.Add(waveSpawns);
		enemySpawns.Add(zigZagSpawns);
		enemySpawns.Add(shootSpawns);
		enemySpawns.Add(jumpSpawns);
	}

	private void SpawnAllEnemies() {
		foreach(Enemies enemyType in Enum.GetValues(typeof(Enemies))) {
			StartCoroutine(SpawnEnemies(enemyType, enemySpawns[(int)enemyType]));
		}
	}

	private IEnumerator SpawnEnemies(Enemies movementPattern, Transform[] spawns) {
		for(int i = 0; i < spawns.Length; i++) {
			if(movementPattern == Enemies.Jump || movementPattern == Enemies.UpDown) {
				yield return new WaitForSeconds(0.3f);
			}
			else {
				yield return null;
			}

			spawner.SpawnEnemy(movementPattern, spawns[i].position);
		}
	}

	private void SpawnPlayer(Vector2 position) {
		spawner.SpawnPlayer(position);
	}

	private void Update() {
		if(GetPlayer() != null) {
			if(GetPlayer().IsDead) {
				if(checkPoint != null) {
					SpawnPlayer(checkPoint.Position);
				}
				else {
					SpawnPlayer(playerSpawn.position);
				}
				GetPlayer().IsDead = false;
			}
		}
	}

	private Player GetPlayer() {
		return GameManager.Instance.Player;
	}

	public void OnFinish(object src, EventArgs e) {
		Spawned = false;
	}
}
