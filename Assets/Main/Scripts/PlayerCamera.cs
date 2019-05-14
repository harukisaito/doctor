using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	[SerializeField] private float offset = -5;

	private Vector3 offsetVec;
	private GroundCheck groundCheck;
	private MovementController movementController;

	private float smoothTime = 0.125f;
	private bool visited;

	private void Start() {
		groundCheck = SpawnManager.Instance.Spawner.PlayerInstance.GetComponent<GroundCheck>();
		movementController = SpawnManager.Instance.Spawner.PlayerInstance.GetComponent<MovementController>();
		offsetVec = new Vector3(3, 0, offset);
		transform.position = GameManager.Instance.Player.transform.position;
	}

	private void FixedUpdate() {
		if(GameManager.Instance.Player != null) {
			Vector3 desiredPosition = GameManager.Instance.Player.transform.position + offsetVec; 
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothTime);
			transform.position = smoothedPosition;
		}
	}

	private void Update() {
		if(!groundCheck.IsGrounded && offsetVec.z > -10) {
			offsetVec.z -= Time.deltaTime;
		}
		else if(movementController.Velocity.y <= 0 && offsetVec.z <= offset) {
			offsetVec.z += Time.deltaTime * 3;
		}
	}
}
