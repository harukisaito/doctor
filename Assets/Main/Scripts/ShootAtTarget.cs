using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CheckForTarget))]
public class ShootAtTarget : MonoBehaviour {

	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private float offsetRadius;
	[SerializeField] private float fireRate;

	private Vector3 offset;
	private CheckForTarget check;
	private Player player;

	private bool shot = false;
	public bool Shooting {get; set;}

	private void Start() {
		check = GetComponent<CheckForTarget>();
		player = GameManager.Instance.Player;
	}

	private void Update() {
		if(check.TargetInRange && !shot) {
			Shooting = true;
			shot = true;
			StartCoroutine(Shoot());
		}
		else if(!check.TargetInRange) {
			Shooting = false;
		}
	}


	private IEnumerator Shoot() {
		while(Shooting) {
			Vector2 playerPos = player.transform.localPosition;
			if(playerPos.x > check.transform.position.x) {
				offset = new Vector3(offsetRadius, 0);
			}
			else if(playerPos.x < check.transform.position.x) {
				offset = new Vector3(-offsetRadius, 0);
			}
			Instantiate(projectilePrefab, check.transform.position + offset, Quaternion.identity);
			yield return new WaitForSeconds(fireRate);
		}
	}
}
