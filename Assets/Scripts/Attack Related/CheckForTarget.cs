using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForTarget : MonoBehaviour {

	[SerializeField] private float enemyCheckRadius = 5f;
	[SerializeField] private float checkFrequency;
	[SerializeField] private LayerMask whatIsTarget;

	public bool TargetInRange {get; set;}

	private void Start() {
		StartCoroutine(LookForTarget());
	}

	private IEnumerator LookForTarget() {
		while(true) {
			TargetInRange = Physics2D.OverlapCircle(transform.position, enemyCheckRadius, whatIsTarget);
			yield return new WaitForSeconds(checkFrequency);
		}
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.localPosition, enemyCheckRadius);
	}
}
