using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {

	private Dictionary<Enemies, Queue> enemyPool = new Dictionary<Enemies, Queue>();
	private Dictionary<FlyingObjects, Queue> flyingObjectPool = new Dictionary<FlyingObjects, Queue>();
	private Dictionary<Projectiles, Queue> projectilePool = new Dictionary<Projectiles, Queue>();
	private Dictionary<Trails, Queue> trailPool = new Dictionary<Trails, Queue>();
	private Dictionary<Particles, Queue> particlePool = new Dictionary<Particles, Queue>();

	private List<Queue> pools = new List<Queue>();

	private Queue leftRightEnemies, leftRightShootEnemies, 
		upDownEnemies, waveEnemies, zigZagEnemies, 
		shootEnemies, jumpEnemies;
	private Queue flyingObjects;
	private Queue projectiles;
	private Queue jumpTrails, dashTrails, stompTrails;
	private Queue runningParticles, jumpParticles, 
		leftRightEnemyParticles, shootEnemyParticles, jumpEnemyParticles, upDownEnemyParticles, 
		playerDamageParticles,
		leftRightDeathParticles, shootEnemyDeathParticles, jumpEnemyDeathParticles, upDownEnemyDeathParticles,
		projectileDestructionParticles;


	public static ObjectPoolManager Instance;
	private void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}

		InstantiateQueues();
		AddToPoolList();
		AddToDictionary();
	}

	private void InstantiateQueues() {
		leftRightEnemies = new Queue();
		leftRightShootEnemies = new Queue();
		upDownEnemies = new Queue();
		waveEnemies = new Queue();
		zigZagEnemies = new Queue();
		shootEnemies = new Queue();
		jumpEnemies = new Queue();
		flyingObjects = new Queue();
		projectiles = new Queue();
		jumpTrails = new Queue();
		dashTrails = new Queue();
		stompTrails = new Queue();
		runningParticles = new Queue();
		jumpParticles = new Queue();
		leftRightEnemyParticles = new Queue();
		shootEnemyParticles = new Queue();
		jumpEnemyParticles = new Queue();
		upDownEnemyParticles = new Queue();
		playerDamageParticles = new Queue();
		leftRightDeathParticles = new Queue();
		shootEnemyDeathParticles = new Queue();
		jumpEnemyDeathParticles = new Queue();
		upDownEnemyDeathParticles = new Queue();
		projectileDestructionParticles = new Queue();
	}

	private void AddToPoolList() {
		pools.Add(leftRightEnemies);
		pools.Add(leftRightShootEnemies);
		pools.Add(upDownEnemies);
		pools.Add(waveEnemies);
		pools.Add(zigZagEnemies);
		pools.Add(shootEnemies);
		pools.Add(jumpEnemies);
		pools.Add(flyingObjects);
		pools.Add(projectiles);
		pools.Add(jumpTrails);
		pools.Add(dashTrails);
		pools.Add(stompTrails);
		pools.Add(runningParticles);
		pools.Add(jumpParticles);
		pools.Add(leftRightEnemyParticles);
		pools.Add(shootEnemyParticles);
		pools.Add(jumpEnemyParticles);
		pools.Add(upDownEnemyParticles);
		pools.Add(playerDamageParticles);
		pools.Add(leftRightDeathParticles);
		pools.Add(shootEnemyDeathParticles);
		pools.Add(jumpEnemyDeathParticles);
		pools.Add(upDownEnemyDeathParticles);
		pools.Add(projectileDestructionParticles);
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

		particlePool.Add(Particles.Running, runningParticles);
		particlePool.Add(Particles.Fall, jumpParticles);
		particlePool.Add(Particles.LeftRightEnemyDamage, leftRightEnemyParticles);
		particlePool.Add(Particles.ShootEnemyDamage, shootEnemyParticles);
		particlePool.Add(Particles.JumpEnemyDamage, jumpEnemyParticles);
		particlePool.Add(Particles.UpDownEnemyDamage, upDownEnemyParticles);
		particlePool.Add(Particles.PlayerDamage, playerDamageParticles);
		particlePool.Add(Particles.LeftRightEnemyDeath, leftRightDeathParticles);
		particlePool.Add(Particles.ShootEnemyDeath, shootEnemyDeathParticles);
		particlePool.Add(Particles.JumpEnemyDeath, jumpEnemyDeathParticles);
		particlePool.Add(Particles.UpDownEnemyDeath, upDownEnemyDeathParticles);
		particlePool.Add(Particles.ProjectileDestrucion, projectileDestructionParticles);
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
	public void AddToObjectPool(Particles key, GameObject gameObject) {
		particlePool[key].Enqueue(gameObject);
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
	public GameObject RetrieveFromObjectPool(Particles key) {
		GameObject g = (GameObject)particlePool[key].Dequeue();
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
	public bool CheckIfEmpty(Particles key) {
		if(particlePool[key].Count == 0) {
			return true;
		}
		else return false;
	}

	private void ResetPools() {
		foreach(Queue q in pools) {
			q.Clear();
		}
	}

	public void OnFinish(object src, EventArgs e) {
		ResetPools();
	}
	// public void OnFinishedLoadingLevel(object src, EventArgs e) {
		// ResetPools();
	// }
}
