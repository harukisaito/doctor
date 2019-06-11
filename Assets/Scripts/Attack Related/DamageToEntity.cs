﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToEntity : MonoBehaviour {

	[SerializeField] private int damage;

	private Entity entity;

	private void OnTriggerEnter2D(Collider2D other) {
		// Debug.Log(other.gameObject.tag);
		if((other.CompareTag("Player") && this.gameObject.tag != "PlayerAttack") 
		|| (other.CompareTag("Enemy") && this.gameObject.tag != "EnemyAttack")) {
			// Debug.Log("DAMAGE");
			
			entity = other.GetComponent<Entity>();
			
			entity.TakeDamage(damage);

			ParticleManager.Instance.SpawnParticles(Particles.Attack, other.transform.position);
		}
		else if(other.CompareTag("Untagged")) {
			// Debug.LogWarning("CHECK THE TAG OF THE OTHER OBJECT");
		}
	}
}
