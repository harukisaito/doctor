using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField] private float speed;

	private Vector3 direction;
	private Transform player;
	private Collider2D projectileCollider;
	private float playerPosX;
	private float lifeTime;

	private void Start() {
		SetDirection();
		projectileCollider = GetComponent<Collider2D>();
	}

	private void Update() {
		transform.localPosition += direction * speed * Time.deltaTime;
		lifeTime += Time.deltaTime;
		if(lifeTime > 5) {
			AddToPool();
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		ParticleManager.Instance.InstantiateParticles(transform.position);
		AddToPool();
	}

	private void AddToPool() {
		ObjectPoolManager.Instance.AddToObjectPool(Keys.Projectiles, gameObject);
		gameObject.SetActive(false);
	}

	private void SetDirection() {
		player = GameManager.Instance.Player.transform;
		Vector3 playerPos = player.position;
		playerPos.y = playerPos.y - 0.5f;
		direction = (playerPos - transform.position).normalized;
	}
}
