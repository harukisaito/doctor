using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {

	[SerializeField] Attack[] attacks;

	private PlayerAnimations playerAnimations;

	private void Start() {
		playerAnimations = GetComponent<PlayerAnimations>();
	}


	public void Attack(AttackPattern attackPattern) {
		int index = (int)attackPattern;
		attacks[index].ActivateAttack();
		playerAnimations.PlayAttackAnimation();
	}
}
