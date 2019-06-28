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
	// private MovementController movementController;
	private Coroutine shoot;
	private GameObject projectileInstance;

	public bool Shooting {get; set;}

	private void Start() {
		check = GetComponent<CheckForTarget>();
		// movementController = GetComponent<MovementController>();
		player = GameManager.Instance.Player;
		// Shooting = true;
		// StartCoroutine(Shoot());
	}

	private void Update() {
		if(check.TargetInRange && !Shooting) {
			Shooting = true;
			// if(movementController != null) {
			// 	movementController.Stop = true;
			// }
			shoot = StartCoroutine(Shoot());
		}
		else if(!check.TargetInRange && Shooting) {
			Shooting = false;
			// if(movementController != null) {
			// 	movementController.Stop = false;
			// }
			if(shoot == null) {
				return;
			}
			StopCoroutine(shoot);
		}
	}


	private IEnumerator Shoot() {
		while(Shooting) {
			Vector2 playerPos = player.transform.position;
			if(playerPos.x > check.transform.position.x) {
				offset = new Vector3(offsetRadius, 0);
			}
			else if(playerPos.x < check.transform.position.x) {
				offset = new Vector3(-offsetRadius, 0);
			}
			yield return new WaitForEndOfFrame();
			SpawnProjectile();
			SceneManagement.Instance.MoveToScene(projectileInstance, Scenes.LevelSakura);
			yield return new WaitForSeconds(fireRate);
		}
	}
	private void SpawnProjectile() {
		bool empty = ObjectPoolManager.Instance.CheckIfEmpty(Projectiles.Basic);
		if(empty) {
			InstantiateProjectile();
		}
		else {
			projectileInstance = ObjectPoolManager.Instance.RetrieveFromObjectPool(Projectiles.Basic);
			projectileInstance.transform.position = transform.position + offset;
			projectileInstance.SetActive(true);
		}
	}

	private void InstantiateProjectile() {
		projectileInstance = Instantiate(projectilePrefab, transform.position + offset, Quaternion.identity);
	}
}
