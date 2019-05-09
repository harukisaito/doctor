﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObjectSpawner : MonoBehaviour {

	[SerializeField] private GameObject flyingObjPrefab;
	[SerializeField] private float spawnRate = 1f; 
	[SerializeField] private bool moveRight;
	[SerializeField] private float speed;

	private GameObject flyingObjInstance;
	private FlyingObject flyingObject;
	private OneDirectionMovement movement;

	private int num = 4;
	private int num2 = 1;


	private void Start() {
		StartCoroutine(SpawnFlyingObject());
	}

	private IEnumerator SpawnFlyingObject() {
		for(;;) {
			bool empty = ObjectPoolManager.Instance.CheckIfEmpty(Keys.FlyingObjects);
			if(empty) {
				flyingObjInstance = Instantiate(flyingObjPrefab, transform.position, Quaternion.identity);
				flyingObject = flyingObjInstance.GetComponent<FlyingObject>();
				flyingObject.Key = Keys.FlyingObjects;
				flyingObject.Id = Random.Range(1f, 100f);
			}
			else {
				flyingObjInstance = ObjectPoolManager.Instance.RetrieveFromObjectPool(Keys.FlyingObjects);
				flyingObjInstance.transform.position = transform.position;
				flyingObjInstance.SetActive(true);
			}

			movement = flyingObjInstance.GetComponent<OneDirectionMovement>();
			movement.MoveRight = moveRight;
			movement.Speed = speed;
			yield return new WaitForSeconds(spawnRate);
		}
	}
}
