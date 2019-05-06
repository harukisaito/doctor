using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[SerializeField] private Player player;
	[SerializeField] private Text hpText;
	[SerializeField] private Transform[] spawnPoints;
	[SerializeField] private Transform spawnPointPlayer;

	private EntitySpawner spawner;
	private CheckPoint checkPoint;
	private Enemy enemy;

	public bool Goal {get; set;}

	public Player Player {
		get { return player; }
		set { player = value; }
	}

	public Enemy Enemy {
		get { return enemy; }
		set { enemy = value; }
	}

	public CheckPoint CheckPoint {
		get { return checkPoint; }
		set { checkPoint = value; }
	}

	public static GameManager Instance;


	private float timer;

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
		spawner.SpawnPlayer(spawnPointPlayer.localPosition);
		SpawnEnemies(spawnPoints);
	}

	private void Update() {
		hpText.text = "HP = " + player.HP.ToString();
		if(player.IsDead) {
			Respawn();
		}
		if(enemy.IsDead) {
			spawner.SpawnEnemy(Random.insideUnitCircle);
		}
		if(Goal) {
			timer += Time.deltaTime;
			if(timer > 6) {
				SceneManager.LoadSceneAsync(0);
				SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
			}
		}
	}

	public void Respawn() {
		if(checkPoint != null) {
			spawner.SpawnPlayer(checkPoint.Position);
		}
		else {
			spawner.SpawnPlayer(spawnPointPlayer.localPosition);
		}
	}

	private void SpawnEnemies(Transform[] spawns) {
		for(int i = 0; i < spawns.Length; i++) {
			spawner.SpawnEnemy(spawns[i].localPosition);
		}
	}
}
