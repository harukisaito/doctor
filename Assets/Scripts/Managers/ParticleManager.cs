using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

	[SerializeField] private GameObject particlePrefab;

	private GameObject particleInstance;
	private EmitParticles particles;

	public static ParticleManager Instance;

	private void Awake() {
		DontDestroyOnLoad(this);
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}

	public void InstantiateParticles(Vector2 position) {
		particleInstance = Instantiate(particlePrefab, position, Quaternion.identity);
		particles = particleInstance.GetComponent<EmitParticles>();
		particles.StartEmitPartices();
	}
}
