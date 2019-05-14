using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitParticles : MonoBehaviour {

	private ParticleSystem particles;

	public void StartEmitPartices() {
		if(particles == null) {
			particles = GetComponent<ParticleSystem>();
		}
		particles.Play();
	}
}
