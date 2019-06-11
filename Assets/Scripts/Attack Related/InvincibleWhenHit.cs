using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
[RequireComponent(typeof(SpriteRenderer))]
public class InvincibleWhenHit : MonoBehaviour {

	[SerializeField] private float blinkingSpeed = 0.05f;
	[SerializeField] private float invincibilityPeriod;

	private Entity entity;
	private SpriteRenderer spriteRenderer;

	private float elapsedTime;
	private bool enable;

	private void Start() {
		entity = GetComponent<Entity>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private IEnumerator StartInvincibility(float invicibilityPeriod) {
		yield return null;
		if(!GameManager.Instance.Player.IsDead) {
			entity.IsInvincible = true;
			Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), true);

			while(invicibilityPeriod >= elapsedTime) {
				enable = !enable;
				spriteRenderer.enabled = enable;
				elapsedTime += blinkingSpeed;
				yield return new WaitForSeconds(blinkingSpeed);
			}

			elapsedTime = 0;
			enable = true;
			entity.IsInvincible = false;
			spriteRenderer.enabled = true;
			Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), false);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("EnemyAttack") || other.gameObject.CompareTag("Enemy")) {
			if(!entity.IsDead) {
				StartCoroutine(StartInvincibility(invincibilityPeriod));
			}
		}
	}
}
