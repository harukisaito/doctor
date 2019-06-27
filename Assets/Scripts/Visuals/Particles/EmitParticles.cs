using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitParticles : MonoBehaviour {

	private ParticleSystem particles;

	public void StartEmitParticles() {
		GetComponent();
		particles.Play();
	}

	public void StopEmitParticles() {
		GetComponent();
		particles.Stop();
	}

	private void GetComponent() {
		if(particles == null) {
			particles = GetComponent<ParticleSystem>();
		}
	}
} 
