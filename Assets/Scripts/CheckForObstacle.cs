using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForObstacle : MonoBehaviour {

	[SerializeField] private Transform check;
	[SerializeField] private float checkRadius = 0.2f;
	[SerializeField] private float checkOffset = 0.5f;
	[SerializeField] private LayerMask whatIsGround;

	private bool obstacle;

	private MovementController movementController;

	private void Start() {
		movementController = GetComponent<MovementController>();
		check.localPosition = new Vector2(checkOffset, -0.256f);
	}

	public bool LookForObstacle() {
		obstacle = Physics2D.OverlapCircle(check.position, checkRadius, whatIsGround);
		if(!obstacle) {
			checkOffset *= -1;
		}
		check.localPosition = new Vector2(checkOffset, -0.256f);
		return obstacle;
	}
}
