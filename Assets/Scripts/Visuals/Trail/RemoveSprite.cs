using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSprite : MonoBehaviour {

	[SerializeField] Trails type;

	public void AddToObjectPool() {
		gameObject.SetActive(false);
		ObjectPoolManager.Instance.AddToObjectPool(type, gameObject);
	}
}
