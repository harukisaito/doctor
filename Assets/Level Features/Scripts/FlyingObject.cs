using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour {

    public Keys Key {get; set;}

    public float Id {get; set;}
    public bool AddedToPool {get; set;}

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "DeathFall" && !AddedToPool) {
            gameObject.SetActive(false);
            ObjectPoolManager.Instance.AddToObjectPool(Key, gameObject);
            AddedToPool = true;
        }
    }
}
