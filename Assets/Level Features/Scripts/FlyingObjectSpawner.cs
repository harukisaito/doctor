using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObjectSpawner : MonoBehaviour {

	[SerializeField] private GameObject flyingObjPrefab;
	[SerializeField] private float spawnRate = 1f; 
	[SerializeField] private float speed = 1f;
	[SerializeField] private bool moveRight;
	private GameObject flyingObjInstance;
	private FlyingObject flyingObject;

	private void Start() {
		StartCoroutine(SpawnFlyingObject());
	}

	private IEnumerator SpawnFlyingObject() {
		for(;;) {
			flyingObjInstance = Instantiate(flyingObjPrefab, transform.position, Quaternion.identity);
			flyingObject = flyingObjInstance.GetComponent<FlyingObject>();
			flyingObject.Length = Random.Range(3f, 6f);
			flyingObject.Height = Random.Range(1f, 2f);
			flyingObject.Speed = speed;
			flyingObject.MovingRight = moveRight;
			yield return new WaitForSeconds(flyingObject.Length * spawnRate);
		}
	}
}
