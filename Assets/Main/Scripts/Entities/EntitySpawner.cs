using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour {

	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private GameObject[] enemyPrefabs;


	private GameObject playerInstance;
	private GameObject enemyInstance;

	public GameObject PlayerInstance {
		get { return playerInstance; }
	}

	public void SpawnPlayer(Vector2 spawnPosition) {
		if(playerInstance == null) {
			playerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
			GameManager.Instance.Player = playerInstance.GetComponent<Player>();
		} 
		else {
			playerInstance.SetActive(true);
			playerInstance.transform.position = spawnPosition;
		}
	}

	public void SpawnEnemy(Keys movementPattern, Vector2 spawnPosition) {
		bool empty = ObjectPoolManager.Instance.CheckIfEmpty(movementPattern);
		if(empty) {
			InstantiateEnemy(movementPattern, spawnPosition);
		} 
		else {
			GameManager.Instance.Enemy = 
				ObjectPoolManager.Instance.RetrieveFromObjectPool(movementPattern).GetComponent<Enemy>();
		}
	}

	private void InstantiateEnemy(Keys movementPattern, Vector2 spawnPosition) {
		enemyInstance = Instantiate(enemyPrefabs[(int)movementPattern], spawnPosition, Quaternion.identity);
		// GameManager.Instance.Enemy = 
		enemyInstance.GetComponent<Enemy>().Key = movementPattern;
	}

}
