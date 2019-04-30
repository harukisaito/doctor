using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField] private float speed;

	private Vector3 direction;
	private float playerPosX;
	private float lifeTime;

	private void Start() {
		playerPosX = GameManager.Instance.Player.transform.localPosition.x;
		
		if(playerPosX > transform.localPosition.x) {
			direction = Vector2.right;
		}
		else if(playerPosX < transform.localPosition.x) {
			direction = Vector2.left;
		}
	}

	private void Update() {
		transform.localPosition += direction * speed * Time.deltaTime;
		lifeTime += Time.deltaTime;
		if(lifeTime > 5) {
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D() {
		Destroy(gameObject);
	}
}
