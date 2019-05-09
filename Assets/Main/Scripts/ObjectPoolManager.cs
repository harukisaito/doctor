using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {

	private Dictionary<Keys, Queue> objectPool = new Dictionary<Keys, Queue>();

	private Queue leftRightEnemies, upDownEnemies, waveEnemies, zigZagEnemies;
	private Queue flyingObjects;
	private Queue projectiles;
	public static ObjectPoolManager Instance;

	private void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}

	private void Start() {
		leftRightEnemies = upDownEnemies = waveEnemies = zigZagEnemies = flyingObjects = projectiles = new Queue();

		objectPool.Add(Keys.LeftRightEnemies, leftRightEnemies);
		objectPool.Add(Keys.UpDownEnemies, upDownEnemies);
		objectPool.Add(Keys.WaveEnemies, waveEnemies);
		objectPool.Add(Keys.ZigZagEnemies, zigZagEnemies);
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
