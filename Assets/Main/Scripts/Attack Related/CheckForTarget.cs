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
		for(;;) {
			TargetInRange = Physics2D.OverlapCircle(transform.localPosition, enemyCheckRadius, whatIsTarget);
			// Debug.Log(TargetInRange);
			yield return new WaitForSeconds(checkFrequency);
		}
		
	}
}
