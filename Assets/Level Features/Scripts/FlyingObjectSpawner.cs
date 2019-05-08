using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObjectSpawner : MonoBehaviour {

	[SerializeField] private GameObject flyingObjPrefab;
	[SerializeField] private float spawnRate = 1f; 
	[SerializeField] private bool moveRight;
	[SerializeField] private float speed;
	private GameObject flyingObjInstance;
	private FlyingObject flyingObject;
	private OneDirectionMovement movement;

	private void Start() {
		StartCoroutine(SpawnFlyingObject());
	}

	private IEnumerator SpawnFlyingObject() {
		for(;;) {
			flyingObjInstance = Instantiate(flyingObjPrefab, transform.position, Quaternion.identity);
			flyingObject = flyingObjInstance.GetComponent<FlyingObject>();
			flyingObject.Length = Random.Range(3f, 6f);
			flyingObject.Height = Random.Range(1f, 2f);

			movement = flyingObjInstance.GetComponent<OneDirectionMovement>();
			movement.MoveRight = moveRight;
			movement.Speed = speed;
			yield return new WaitForSeconds(flyingObject.Length * spawnRate);
		}
	}
}
