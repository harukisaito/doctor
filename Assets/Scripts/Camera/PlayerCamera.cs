using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	[SerializeField] private Transform leftDeadZone;
	[SerializeField] private Transform rightDeadZone;
	[SerializeField] private Transform downDeadZone;

	[SerializeField] private float offset = -5;

	private Vector3 offsetVec;
	private Vector3 smoothedPosition;
	private GroundCheck groundCheck;
	private MovementController movementController;
	private Player player;

	private float smoothTime = 0.125f;
	private float leftDead;
	private float rightDead;
	private float downDead;
	private Vector2 tempPos;

	public static PlayerCamera Instance;
	private void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}

	public void Initialize() {
		groundCheck = SpawnManager.Instance.Spawner.PlayerInstance.GetComponent<GroundCheck>();
		movementController = SpawnManager.Instance.Spawner.PlayerInstance.GetComponent<MovementController>();
		player = GameManager.Instance.Player;
		transform.position = GameManager.Instance.Player.transform.position;
		offsetVec = new Vector3(3, 0, offset);
		leftDead = leftDeadZone.position.x;
		rightDead = rightDeadZone.position.x;
		downDead = downDeadZone.position.y;
	}

	private void FixedUpdate() {
		if(player != null) {
			Vector2 playerPos = player.transform.position;
			if(playerPos.x < leftDead || playerPos.x > rightDead) {
				Vector3 desiredPosition = new Vector3(tempPos.x, playerPos.y) + offsetVec;
				smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothTime);
			}
			else if(playerPos.y < downDead) {
				Vector3 desiredPosition = new Vector3(playerPos.x, tempPos.y) + offsetVec;
				smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothTime);
			}
			else {
				Vector3 desiredPosition = player.transform.position + offsetVec; 
				smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothTime);
				tempPos = playerPos;
			}
			transform.position = smoothedPosition;
		}
	}

	private void Update() {
		if(SpawnManager.Instance.Spawned) {
			if(!groundCheck.IsGrounded && offsetVec.z > -10) {
				offsetVec.z -= Time.deltaTime;
			}
			else if(movementController.Velocity.y <= 0 && offsetVec.z <= offset) {
				offsetVec.z += Time.deltaTime * 3;
			}
		}
	}
}
