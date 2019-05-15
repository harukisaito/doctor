using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {

	[SerializeField] Attack[] attacks;

	public void Attack(AttackPattern attackPattern) {
		int index = (int)attackPattern;
		Debug.Log(index);
		attacks[index].ActivateAttack();
	}
}
