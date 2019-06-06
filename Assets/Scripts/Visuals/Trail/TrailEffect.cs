using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour {
	[SerializeField] private GameObject jumpSprite;
	[SerializeField] private GameObject dashSprite;
	[SerializeField] private GameObject stompSprite;
	[SerializeField] private float trailLength;

	private Queue jumpInstances;
	private Queue dashInstances;
	private Queue stompInstances;

	private List<Queue> instances = new List<Queue>();
	private List<GameObject> sprites = new List<GameObject>();

	private void Start() {
		instances.Add(jumpInstances);
		instances.Add(dashInstances);
		instances.Add(stompInstances);
	}

    private IEnumerator SpawnSprites(Trails key, float spawnRate) {
		while(true) {
			yield return new WaitForSeconds(spawnRate);
			bool empty = ObjectPoolManager.Instance.CheckIfEmpty(key);
			if(empty) {
				InstantiateSprites(key); 
			}
			else {
				GameObject instance = ObjectPoolManager.Instance.RetrieveFromObjectPool(key);
				instance.transform.position = transform.position;
				instances[(int)key].Enqueue(instance);
			}
			RemoveSprite(key);
		}
	}

	private void InstantiateSprites(Trails key) {
		int index = (int)key;
		instances[index].Enqueue(Instantiate(sprites[index], transform.position, Quaternion.identity));
	}

	private void RemoveSprite(Trails key) {
		int index = (int)key;
		GameObject instance = (GameObject)instances[index].Dequeue();
		StartCoroutine(AddToObjectPool(key, instance));
	}

	private IEnumerator AddToObjectPool(Trails key, GameObject trail) {
		yield return new WaitForSeconds(trailLength);
		ObjectPoolManager.Instance.AddToObjectPool(key, trail);
	}

	public void Jump() {
		
	}

	public void Dash() {
	}

	public void Stomp() {
	}
}
