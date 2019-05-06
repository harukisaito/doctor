using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInAir : MonoBehaviour {

	private GroundCheck groundCheck;
	private FlyingBehaviour platform;

	void Start () {
		groundCheck = GetComponent<GroundCheck>();
	}
	
	void Update () {
		if(!groundCheck.IsGrounded) {
			if(platform != null) {
				if(!platform.MoveRight) {
					Debug.Log("Moving Left");
					transform.localPosition += Vector3.left * platform.Speed * Time.deltaTime;
				}
				else if(platform.MoveRight) {
					Debug.Log("Moving Right");
					Debug.Log(platform.Speed);
					transform.localPosition += Vector3.right * platform.Speed * Time.deltaTime;
				}
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Flying Object") {
			platform = other.gameObject.GetComponent<FlyingBehaviour>();
		}
	}
}
