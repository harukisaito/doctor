using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

	[SerializeField] private GameObject[] particles;

	private GameObject particleInstance;
	private EmitParticles particleEmit;

	public static ParticleManager Instance;

	private void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}

	public void SpawnParticles(Particles type, Vector2 position) {
		bool empty = ObjectPoolManager.Instance.CheckIfEmpty(type);
		if(empty) {
			InstantiateParticles(type, position);
		}
		else {
			particleInstance = ObjectPoolManager.Instance.RetrieveFromObjectPool(type);
			particleInstance.transform.position = position;
			particleEmit = particleInstance.GetComponent<EmitParticles>();
		}
		particleEmit.StartEmitPartices();
		StartCoroutine(RetrieveParticles(type, particleInstance));
	}

	private void InstantiateParticles(Particles type, Vector2 position) {
		int index = (int)type;
		particleInstance = Instantiate(particles[index], position, Quaternion.identity);
		SceneManagement.Instance.MoveToScene(particleInstance, Scenes.LevelSakura);
		particleEmit = particleInstance.GetComponent<EmitParticles>();
	}

	private IEnumerator RetrieveParticles(Particles type, GameObject particle) {
		yield return new WaitForSeconds(5f);
		ObjectPoolManager.Instance.AddToObjectPool(type, particle);
	}

}
