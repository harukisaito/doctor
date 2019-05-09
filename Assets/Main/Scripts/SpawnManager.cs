using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	[SerializeField] private Transform playerSpawn;

	[SerializeField] private Transform[] leftRightSpawns, upDownSpawns, waveSpawns, zigZagSpawns;

	private EntitySpawner spawner;
	private CheckPoint checkPoint;

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
		SpawnPlayer(playerSpawn.position);
		SpawnEnemies(Keys.UpDownEnemies, upDownSpawns);
		SpawnEnemies(Keys.WaveEnemies, waveSpawns);
		// SpawnEnemies(MovementPattern.UpDownMovement, upDownSpawns);
		// SpawnEnemies(MovementPattern.UpDownMovement, upDownSpawns);
	}

	private void SpawnEnemies(Keys movementPattern, Transform[] spawns) {
		for(int i = 0; i < spawns.Length; i++) {
			spawner.SpawnEnemy(movementPattern, spawns[i].position);
		}
	}

	private void SpawnPlayer(Vector2 position) {
		spawner.SpawnPlayer(position);
	}

	private void Update() {
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

	private Player GetPlayer() {
		return GameManager.Instance.Player;
	}
}
