using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour {

	[SerializeField] private Transform particleSpawnPoint;
	[SerializeField] private float runningSpawnInterval;
	[SerializeField] private float walkingSpawnInterval;

	private Particles runningParticle = Particles.Running;
	private Particles fallingParticle = Particles.Fall;

	private Coroutine walkingCoroutine;
	private Coroutine runningCoroutine;
	private Coroutine fallingCoroutine;
	private WaitForSeconds runningInterval;
	private WaitForSeconds walkingInterval;
	private Quaternion goingRightRotation;
	private Quaternion goingLeftRotation;
	private Quaternion fallingRotation;

	private Rigidbody2D body;
	private MovementController movementController;
	private GroundCheck check;

	private bool walkingCoroutineRunning;
	private bool runningCoroutineRunning;

	private void Start() {
		GetComponents();

		runningInterval = new WaitForSeconds(runningSpawnInterval);
		walkingInterval = new WaitForSeconds(walkingSpawnInterval);

		goingRightRotation = Quaternion.Euler(0, 0, 90);
		goingLeftRotation = Quaternion.Euler(0, 0, 30);
		fallingRotation = Quaternion.Euler(0, 0, 45);
	}

	private void Update() {
		Vector2 velocity = body.velocity;
		float velX = Mathf.Abs(velocity.x);
		if(check.IsGrounded) {
			if(velX == 2.25f) {
				if(!walkingCoroutineRunning) {
					StopRunningCoroutine();
					walkingCoroutine = StartCoroutine(SpawnParticles(running: false));
				}
			} else
			if(velX == 4.5f) {
				if(!runningCoroutineRunning) {
					StopWalkingCoroutine();
					runningCoroutine = StartCoroutine(SpawnParticles(running: true));
				}
			} else 
			if(velX == 0) {
				StopCurrentCoroutine();
			}
		}
		else {
			StopCurrentCoroutine();	
		}
	}

	private IEnumerator SpawnParticles(bool running) {
		WaitForSeconds interval;
		if(running) {
			runningCoroutineRunning = true;
			interval = runningInterval;
		}
		else {
			walkingCoroutineRunning = true;
			interval = walkingInterval;
		}
		while(true) {
			Quaternion rotation;
			float velX = body.velocity.x;

			if(velX > 0) {
				rotation = goingRightRotation;
			}
			else {
				rotation = goingLeftRotation;
			}

			ParticleManager.Instance.SpawnParticles(
				type: runningParticle, 
				position: particleSpawnPoint.position, 
				rotation: rotation);
			AudioManager.Instance.Play("Footstep");
			yield return interval;
		}
	}

	private void StopCurrentCoroutine() {
		if(runningCoroutineRunning) {
			StopRunningCoroutine();
		}
		else {
			StopWalkingCoroutine();
		}
	}

	private void StopWalkingCoroutine() {
		if(walkingCoroutine != null) {
			StopCoroutine(walkingCoroutine);
			walkingCoroutineRunning = false;
		}
	}
	private void StopRunningCoroutine() {
		if(runningCoroutine != null) {
			StopCoroutine(runningCoroutine);
			runningCoroutineRunning = false;
		}
	}

	private void GetComponents() {
		body = GetComponent<Rigidbody2D>();
		movementController = GetComponent<MovementController>();
		check = GetComponent<GroundCheck>();
	}

	public void OnLanded(object src, EventArgs e) {
		ParticleManager.Instance.SpawnParticles(fallingParticle, particleSpawnPoint.position, fallingRotation);
		AudioManager.Instance.Play("Landed");
	}

	public void OnFinishAnimation(object src, EventArgs e) {
		StopCurrentCoroutine();
	}
}
