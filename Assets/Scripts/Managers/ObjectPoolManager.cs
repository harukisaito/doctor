using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {

	private Dictionary<Keys, Queue> objectPool = new Dictionary<Keys, Queue>();

	private Queue leftRightEnemies, leftRightShootEnemies, upDownEnemies, waveEnemies, zigZagEnemies, shootEnemies, jumpEnemies;
	private Queue flyingObjects;
	private Queue projectiles;
	public static ObjectPoolManager Instance;

	private void Awake() {
		DontDestroyOnLoad(this);
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
		leftRightEnemies = leftRightShootEnemies = upDownEnemies = waveEnemies = zigZagEnemies = shootEnemies = jumpEnemies = flyingObjects = projectiles = new Queue();

		objectPool.Add(Keys.LeftRightEnemies, leftRightEnemies);
		objectPool.Add(Keys.LeftRightShootEnemies, leftRightShootEnemies);
		objectPool.Add(Keys.UpDownEnemies, upDownEnemies);
		objectPool.Add(Keys.WaveEnemies, waveEnemies);
		objectPool.Add(Keys.ZigZagEnemies, zigZagEnemies);
		objectPool.Add(Keys.ShootEnemies, shootEnemies);
		objectPool.Add(Keys.JumpEnemies, jumpEnemies);
		objectPool.Add(Keys.FlyingObjects, flyingObjects);
		objectPool.Add(Keys.Projectiles, projectiles);
	}

	public void AddToObjectPool(Keys key, GameObject gameObject) {
		objectPool[key].Enqueue(gameObject);
	}

	public GameObject RetrieveFromObjectPool(Keys key) {
		GameObject g = (GameObject)objectPool[key].Dequeue();
		return g;
	}

	public bool CheckIfEmpty(Keys key) {
		if(objectPool[key].Count == 0) {
			return true;
		}
		else {
			return false;
		}
	}
}
