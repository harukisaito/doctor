using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour {

	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private GameObject enemyPrefab;

	private GameObject playerInstance;
	private GameObject enemyInstance;

	public void SpawnPlayer(Vector2 spawnPosition) {
		playerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
		GameManager.Instance.Player = playerInstance.GetComponent<Player>();
	}

	public void SpawnEnemy(Vector2 spawnPosition) {
		enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
		GameManager.Instance.Enemy = enemyInstance.GetComponent<Enemy>();
	}
}
