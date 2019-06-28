using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

	[SerializeField] private int hp = 2;
	[SerializeField] private bool isInvincible;

	private int setHp;
	private bool isDead;
	private EnemyDamageAnimations damageAnimation;

	public Enemies Key {get; set;}
	public override Particles ParticleTypeDamage {get; set;}
	public Particles ParticleTypeDeath {get; set;}

	public override bool IsInvincible {
		get {return isInvincible;}
		set {isInvincible = true;}
	}

	public override int HP {
		get { return hp; } 
		set { 
			hp = value;
			TakeDamage(value); 

		}
	}

	public override bool IsDead {
		get { return isDead; }
		set {
			isDead = value; 
		}
	}

	private void Start() {
		damageAnimation = GetComponent<EnemyDamageAnimations>();
		foreach(Particles particleType in Enum.GetValues(typeof(Particles))) {
			string enemyName = particleType.ToString().Replace("EnemyDamage", "");
			if(enemyName == Key.ToString()) {
				ParticleTypeDamage = particleType;
			}
			string enemyName2 = particleType.ToString().Replace("EnemyDeath", "");
			if(enemyName2 == Key.ToString()) {
				ParticleTypeDeath = particleType;
			}
		}
		setHp = hp;
	}

	public override void TakeDamage(int damage) {
		if(!isInvincible) {
			damageAnimation.IndicateDamage();
			PlayDamageSound(Key);
			hp -= damage;
			isInvincible = true;
			StartCoroutine(ResetInvincibility());

			if(hp <= 0) {
				Die();
			}
		}
	}

	private IEnumerator ResetInvincibility() {
		yield return new WaitForSeconds(0.288f);
		isInvincible = false;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("DeathFall")) {
			Die();
		}
	}

	public override void Die() {
		SpawnParticles();
		isDead = true;
		ObjectPoolManager.Instance.AddToObjectPool(Key, gameObject);
		gameObject.SetActive(false);
	}

	private void SpawnParticles() {
		ParticleManager.Instance.SpawnParticles(ParticleTypeDeath, transform.position, Quaternion.identity);
	}

	private void PlayDamageSound(Enemies key) {
		if(key == Enemies.LeftRight) {
			AudioManager.Instance.Play("Left Right Enemy");
		} else 
		if(key == Enemies.Shoot) {
			AudioManager.Instance.Play("Shoot Enemy");
		} else 
		if(key == Enemies.Jump || key == Enemies.UpDown) {
			AudioManager.Instance.Play("Up Down");
		}
	} 

}
