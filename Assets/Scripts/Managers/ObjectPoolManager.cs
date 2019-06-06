using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {

	private Dictionary<Enemies, Queue> enemyPool = new Dictionary<Enemies, Queue>();
	private Dictionary<FlyingObjects, Queue> flyingObjectPool = new Dictionary<FlyingObjects, Queue>();
	private Dictionary<Projectiles, Queue> projectilePool = new Dictionary<Projectiles, Queue>();
	private Dictionary<Trails, Queue> trailPool = new Dictionary<Trails, Queue>();


	private Queue leftRightEnemies, leftRightShootEnemies, upDownEnemies, waveEnemies, zigZagEnemies, shootEnemies, jumpEnemies;
	private Queue flyingObjects;
	private Queue projectiles;
	private Queue jumpTrails, dashTrails, stompTrails;
	public static ObjectPoolManager Instance;

	private void Awake() {
		DontDestroyOnLoad(this);
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}

		InstantiateQueues();
		AddToDictionary();
	}

	private void InstantiateQueues() {
		leftRightEnemies = 
		leftRightShootEnemies = 
		upDownEnemies = 
		waveEnemies = 
		zigZagEnemies = 
		shootEnemies = 
		jumpEnemies = 
		flyingObjects = 
		projectiles = 
		jumpTrails = 
		dashTrails = 
		stompTrails = new Queue();
	}

	private void AddToDictionary() {
		enemyPool.Add(Enemies.LeftRight, leftRightEnemies);
		enemyPool.Add(Enemies.LeftRightShoot, leftRightShootEnemies);
		enemyPool.Add(Enemies.UpDown, upDownEnemies);
		enemyPool.Add(Enemies.Wave, waveEnemies);
		enemyPool.Add(Enemies.ZigZag, zigZagEnemies);
		enemyPool.Add(Enemies.Shoot, shootEnemies);
		enemyPool.Add(Enemies.Jump, jumpEnemies);

		flyingObjectPool.Add(FlyingObjects.Basic, flyingObjects);

		projectilePool.Add(Projectiles.Basic, projectiles);

		trailPool.Add(Trails.Jump, jumpTrails);
		trailPool.Add(Trails.Dash, dashTrails);
		trailPool.Add(Trails.Stomp, stompTrails);
	}

	public void AddToObjectPool(Enemies key, GameObject gameObject) {
		enemyPool[key].Enqueue(gameObject);
	}
	public void AddToObjectPool(FlyingObjects key, GameObject gameObject) {
		flyingObjectPool[key].Enqueue(gameObject);
	}
	public void AddToObjectPool(Projectiles key, GameObject gameObject) {
		projectilePool[key].Enqueue(gameObject);
	}
	public void AddToObjectPool(Trails key, GameObject gameObject) {
		trailPool[key].Enqueue(gameObject);
	}



	public GameObject RetrieveFromObjectPool(Enemies key) {
		GameObject g = (GameObject)enemyPool[key].Dequeue();
		return g;
	}
	public GameObject RetrieveFromObjectPool(FlyingObjects key) {
		GameObject g = (GameObject)flyingObjectPool[key].Dequeue();
		return g;
	}
	public GameObject RetrieveFromObjectPool(Projectiles key) {
		GameObject g = (GameObject)projectilePool[key].Dequeue();
		return g;
	}
	public GameObject RetrieveFromObjectPool(Trails key) {
		GameObject g = (GameObject)trailPool[key].Dequeue();
		return g;
	}

	

	public bool CheckIfEmpty(Enemies key) {
		if(enemyPool[key].Count == 0) {
			return true;
		}
		else return false;
	}
	public bool CheckIfEmpty(FlyingObjects key) {
		if(flyingObjectPool[key].Count == 0) {
			return true;
		}
		else return false;
	}
	public bool CheckIfEmpty(Projectiles key) {
		if(projectilePool[key].Count == 0) {
			return true;
		}
		else return false;
	}
	public bool CheckIfEmpty(Trails key) {
		if(trailPool[key].Count == 0) {
			return true;
		}
		else return false;
	}
}
