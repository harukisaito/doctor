using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

	[SerializeField] private GameObject particlePrefab;

	private GameObject particleInstance;
	private EmitParticles particles;

	public static ParticleManager Instance;

	private void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}

	public void InstantiateParticles(Vector2 position, float emitPeriod) {
		particleInstance = Instantiate(particlePrefab, position, Quaternion.identity);
		particles = particleInstance.GetComponent<EmitParticles>();
		particles.StartEmitPartices();
		StartCoroutine(StopParticles(emitPeriod));
	}

	private IEnumerator StopParticles(float emitPeriod) {
		yield return new WaitForSeconds(emitPeriod);
		particles.StopEmitParticles();
	}
}
