using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CheckForTarget))]
public class ShootAtTarget : MonoBehaviour {

	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private float offsetRadius;
	[SerializeField] private float checkTime;

	private Vector3 offset;
	private CheckForTarget check;

	private void Start() {
		check = GetComponent<CheckForTarget>();
		StartCoroutine(LookForTarget());
	}

	private IEnumerator LookForTarget() {
		for(;;) {
			if(check.LookForTarget()) {
				Shoot();
			}
			yield return new WaitForSeconds(checkTime);
		}
	}

	public void Shoot() {
		Vector2 playerPos = GameManager.Instance.Player.transform.localPosition;
		if(playerPos.x > check.transform.position.x) {
			offset = new Vector3(offsetRadius, 0);
		}
		else if(playerPos.x < check.transform.position.x) {
			offset = new Vector3(-offsetRadius, 0);
		}

		Instantiate(projectilePrefab, check.transform.position + offset, Quaternion.identity);
	}
}
