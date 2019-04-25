using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private float checkRadius;
	[SerializeField] private float checkTime;
	[SerializeField] private LayerMask whatIsTarget;

	private List<GameObject> projectiles = new List<GameObject>();

	private bool targetInRange;
	private Vector3 offset;

	private void Start () {
		StartCoroutine(CheckForTarget());
	}

	private IEnumerator CheckForTarget() {
		for(;;) {
			targetInRange = Physics2D.OverlapCircle(transform.localPosition, checkRadius, whatIsTarget);
			if(targetInRange) {
				ShootAtTarget();
			}
			yield return new WaitForSeconds(checkTime);
		}
	}

	private void ShootAtTarget() {
		Vector2 playerPos = GameManager.Instance.Player.transform.localPosition;
		if(playerPos.x > transform.localPosition.x) {
			offset = new Vector3(1, 0);
		}
		else if(playerPos.x < transform.localPosition.x) {
			offset = new Vector3(-1, 0);
		}

		Instantiate(projectilePrefab, transform.localPosition + offset, Quaternion.identity);
	}

}
