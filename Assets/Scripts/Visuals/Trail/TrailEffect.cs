﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour {
	[SerializeField] private GameObject jumpSprite;
	[SerializeField] private GameObject dashSprite;
	[SerializeField] private GameObject stompSprite;
	[SerializeField] private float trailLength;

	private Coroutine jumpCoroutine, dashCoroutine, stompCoroutine;
	private List<GameObject> sprites = new List<GameObject>();

	private MovementController movementController;

	private bool jumpStarted;
	private bool dashStarted;
	private bool stompStarted;

	private void Awake() {
		movementController = GetComponent<MovementController>();
		sprites.Add(jumpSprite);
		sprites.Add(dashSprite);
		sprites.Add(stompSprite);
	}

	private void Update() {
		// Jump();
		Dash();
		Stomp();
	}

    private IEnumerator SpawnSprites(Trails key, float spawnRate) {
		while(true) {
			yield return new WaitForSeconds(spawnRate);
			bool empty = ObjectPoolManager.Instance.CheckIfEmpty(key);
			if(empty) {
				InstantiateSprites(key); 
			}
			else {
				GameObject instance = ObjectPoolManager.Instance.RetrieveFromObjectPool(key);
				instance.transform.position = transform.position;
				instance.GetComponent<SpriteRenderer>().flipX = !movementController.IsFacingRight;
				instance.SetActive(true);
			}
		}
	}

	private void InstantiateSprites(Trails key) {
		GameObject instance = Instantiate(sprites[(int)key], transform.position, Quaternion.identity);
		SceneManagement.Instance.MoveToScene(instance, Scenes.LevelSakura);
		instance.GetComponent<SpriteRenderer>().flipX = !movementController.IsFacingRight;
	}


	// private void Jump() {
	// 	if(movementController.IsJumping) {
	// 		if(!jumpStarted) {
	// 			jumpCoroutine = StartCoroutine(SpawnSprites(Trails.Jump, 0.05f));
	// 			jumpStarted = true;
	// 		}
	// 	}
	// 	else {
	// 		if(jumpStarted) {
	// 			StopCoroutine(jumpCoroutine);
	// 			jumpStarted = false;
	// 		}
	// 	}
	// }

	private void Dash() {
		if(movementController.IsDashing) {
			if(!dashStarted) {
				dashCoroutine =  StartCoroutine(SpawnSprites(Trails.Dash, 0.07f));
				dashStarted = true;
			}
		}
		else {
			if(dashStarted) {
				StopCoroutine(dashCoroutine);
				dashStarted = false;
			}
		}
	}

	private void Stomp() {
		if(movementController.IsStomping) {
			if(!stompStarted) {
				stompCoroutine =  StartCoroutine(SpawnSprites(Trails.Stomp, 0.05f));
				stompStarted = true;
			}
		}
		else {
			if(stompStarted) {
				StopCoroutine(stompCoroutine);
				stompStarted = false;
			}
		}
	}
}
