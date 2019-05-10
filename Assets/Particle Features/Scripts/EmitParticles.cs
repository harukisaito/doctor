using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitParticles : MonoBehaviour {

	private ParticleSystem particles;

	public void StartEmitPartices() {
		particles = GetComponent<ParticleSystem>();
		particles.Play();
	}

	public void StopEmitParticles() {
		particles.Stop();
	}
}
