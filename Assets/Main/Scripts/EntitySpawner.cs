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

	public void SpawnEnemy(int index, Vector2 spawnPosition) {
		InstantiateEnemy(index, spawnPosition);
	}

	private void InstantiateEnemy(int index, Vector2 spawnPosition) {
		enemyInstance = Instantiate(enemyPrefabs[index], spawnPosition, Quaternion.identity);
		GameManager.Instance.Enemy = enemyInstance.GetComponent<Enemy>();
	}

}
