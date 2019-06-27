using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField] private float speed;

	private Vector3 direction;
	private Transform player;
	private float lifeTime;

	private void Start() {
		SetDirection();
	}

	private void OnEnable() {
		SetDirection();
		lifeTime = 0f;
	}

	private void Update() {
		transform.localPosition += direction * speed * Time.deltaTime;
		lifeTime += Time.deltaTime;
		if(lifeTime > 5) {
			lifeTime = 0;
			AddToPool();
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		AudioManager.Instance.Play("Projectile");
		AddToPool();
	}

	private void AddToPool() {
		ObjectPoolManager.Instance.AddToObjectPool(Projectiles.Basic, gameObject);
		gameObject.SetActive(false);
	}

	private void SetDirection() {
		player = GameManager.Instance.Player.transform;
		Vector3 playerPos = player.position;
		playerPos.y = playerPos.y - 0.5f;
		direction = (playerPos - transform.position).normalized;
	}
}
