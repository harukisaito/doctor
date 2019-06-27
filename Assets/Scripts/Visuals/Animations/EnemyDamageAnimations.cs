using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageAnimations : MonoBehaviour {

	private SpriteRenderer sprite;
	private MovementController movementController;
	private Vector3 originalScale;
	private KnockbackDirection knockbackDirection;

	private void Start() {
		sprite = GetComponent<SpriteRenderer>();
		movementController = GetComponent<MovementController>();
		originalScale = transform.localScale;
	}

	public void IndicateDamage() {
		sprite.color = Color.red;
		transform.localScale = originalScale * 0.75f;
		SetKnockBackDirection();
		movementController.Knockback(knockbackDirection, 2, 1);
		StartCoroutine(ResetSpriteColor());
	}

	private IEnumerator ResetSpriteColor() {
		yield return new WaitForSeconds(0.1f);
		sprite.color = Color.white;
		transform.localScale = originalScale;
	}

	private void SetKnockBackDirection() {
		if(GameManager.Instance.Player.transform.position.x < transform.position.x) {
			knockbackDirection = KnockbackDirection.Right;
		}
		else {
			knockbackDirection = KnockbackDirection.Left;
		}
	}
}
