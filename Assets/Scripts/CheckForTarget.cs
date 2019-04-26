using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForTarget : MonoBehaviour {

	[SerializeField] private float enemyCheckRadius = 5f;
	[SerializeField] private LayerMask whatIsTarget;

	private bool targetInRange;

	public bool LookForTarget() {
		targetInRange = Physics2D.OverlapCircle(transform.localPosition, enemyCheckRadius, whatIsTarget);
		if(targetInRange) {
			return true;
		}
		else {
			return false;
		}
	}
}
