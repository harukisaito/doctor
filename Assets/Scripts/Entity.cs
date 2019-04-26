using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

	public abstract int HP {get; set;}

	public abstract void TakeDamage(int damage);

	public abstract void Die();
}
