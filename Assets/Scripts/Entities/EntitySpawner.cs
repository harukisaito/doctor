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
			SceneManagement.Instance.MoveToScene(playerInstance, Scenes.LevelSakura);
			GameManager.Instance.Player = playerInstance.GetComponent<Player>();
		} 
		else {
			playerInstance.SetActive(true);
			playerInstance.transform.position = spawnPosition;
			MovementController movementController = playerInstance.GetComponent<MovementController>();
			movementController.IsStomping = false;
		}
	}

	public void SpawnEnemy(Enemies movementPattern, Vector2 spawnPosition) {
		bool empty = ObjectPoolManager.Instance.CheckIfEmpty(movementPattern);
		if(empty) {
			InstantiateEnemy(movementPattern, spawnPosition);
		} 
		else {
			enemyInstance = ObjectPoolManager.Instance.RetrieveFromObjectPool(movementPattern);
		}
	}

	private void InstantiateEnemy(Enemies movementPattern, Vector2 spawnPosition) {
		enemyInstance = Instantiate(enemyPrefabs[(int)movementPattern], spawnPosition, Quaternion.identity);
		SceneManagement.Instance.MoveToScene(enemyInstance, Scenes.LevelSakura);
		enemyInstance.GetComponent<Enemy>().Key = movementPattern;
	}
}
