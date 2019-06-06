using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour {

    [SerializeField] private bool moveRight;
    [SerializeField] private float speed;

    public float Id {get; set;}
    public bool AddedToPool {get; set;}

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("DeathFall") && !AddedToPool) {
            gameObject.SetActive(false);
            ObjectPoolManager.Instance.AddToObjectPool(FlyingObjects.Basic, gameObject);
            AddedToPool = true;
        }
    }

    private void FixedUpdate() {
        if(moveRight) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        } else {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}
