using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

	[SerializeField] private GameObject[] particles;

	private GameObject particleInstance;
	private WaitForSeconds waitTime = new WaitForSeconds(0.5f);
	private EmitParticles particleEmit;
	private Queue particleInstances = new Queue();

	public static ParticleManager Instance;

	private void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}

	public void SpawnParticles(Particles type, Vector2 position, Quaternion rotation) {
		bool empty = ObjectPoolManager.Instance.CheckIfEmpty(type);
		if(empty) {
			InstantiateParticles(type, position, rotation);
		}
		else {
			particleInstance = ObjectPoolManager.Instance.RetrieveFromObjectPool(type);
			particleInstances.Enqueue(particleInstance);
			particleInstance.transform.position = position;
			particleInstance.transform.rotation = rotation;
			particleInstance.GetComponent<EmitParticles>().StartEmitParticles();
		}
		StartCoroutine(RetrieveParticles(type));
	}

	private void InstantiateParticles(Particles type, Vector2 position, Quaternion rotation) {
		int index = (int)type;
		particleInstance = Instantiate(particles[index], position, rotation);
		particleInstances.Enqueue(particleInstance);
		SceneManagement.Instance.MoveToScene(particleInstance, Scenes.LevelSakura);
		particleEmit = particleInstance.GetComponent<EmitParticles>();
	}

	private IEnumerator RetrieveParticles(Particles type) {
		yield return waitTime;
		AddToObjectPool(type);
	}

	public void OnFinishAnimation(object src, EventArgs e) {
		particleInstances.Clear();
		StopAllCoroutines();
	}

	private void AddToObjectPool(Particles type) {
		if(particleInstances != null) {
			GameObject particle = (GameObject)particleInstances.Dequeue();
			particle.GetComponent<EmitParticles>().StopEmitParticles();
			ObjectPoolManager.Instance.AddToObjectPool(type, particle);
		}
	}

}
