using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
[RequireComponent(typeof(SpriteRenderer))]
public class InvincibleWhenHit : MonoBehaviour {

	[SerializeField] private float blinkingSpeed = 0.05f;

	private Entity entity;
	private SpriteRenderer spriteRenderer;

	private float elapsedTime;
	private bool enable;

	private void Start() {
		entity = GetComponent<Entity>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void StartInvincibility(float invicibilityPeriod) {
		if(!GameManager.Instance.Player.IsDead) {
			StartCoroutine(BlinkSpriteRenderer(invicibilityPeriod));
		}
	}

	private IEnumerator BlinkSpriteRenderer(float invicibilityPeriod) {
		entity.IsInvincible = true;

		while(invicibilityPeriod >= elapsedTime) {
			enable = !enable;
			spriteRenderer.enabled = enable;
			elapsedTime += Time.deltaTime;
			yield return new WaitForSeconds(blinkingSpeed);
		}

		elapsedTime = 0;
		enable = true;
		entity.IsInvincible = false;
		spriteRenderer.enabled = enable;
	}
}
