using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	[SerializeField] private float offset = -5;

	private Vector3 offsetVec;
	private GroundCheck groundCheck;
	private MovementController movementController;
	private Player player;

	private float smoothTime = 0.125f;

	private void Start() {
		groundCheck = SpawnManager.Instance.Spawner.PlayerInstance.GetComponent<GroundCheck>();
		movementController = SpawnManager.Instance.Spawner.PlayerInstance.GetComponent<MovementController>();
		player = GameManager.Instance.Player;
		transform.position = GameManager.Instance.Player.transform.position;
		offsetVec = new Vector3(3, 0, offset);
	}

	private void FixedUpdate() {
		if(player != null) {
			Vector3 desiredPosition = player.transform.position + offsetVec; 
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
		// if(movementController.MovementDirection < 0) {
		// 	offsetVec = new Vector3(-3, 0, offset);
		// 	smoothTime = 0.04f;
		// } else
		// if(movementController.MovementDirection > 0) {
		// 	offsetVec = new Vector3(3, 0, offset);
		// 	smoothTime = 0.04f;
		// }
	}
}
