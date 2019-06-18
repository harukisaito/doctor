using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForObstacle : MonoBehaviour {

	[SerializeField] private Transform check;
	[SerializeField] private float checkRadius = 0.2f;
	[SerializeField] private float checkOffset = 0.5f;
	[SerializeField] private LayerMask whatIsGround;
	[SerializeField] private LayerMask whatIsWall;


	private bool obstacle;
	private bool wall;

	private Vector2 checkPosition;

	public float CheckPositionX {
		get {return checkPosition.x;}
		set {
			checkPosition.x = value;
			check.localPosition = checkPosition;
		}
	}

	private void Start() {
		checkPosition = new Vector2(-checkOffset, check.localPosition.y);
		check.position = checkPosition;
	}

	public bool LookForObstacle() {
		obstacle = Physics2D.OverlapCircle(check.position, checkRadius, whatIsGround);
		return obstacle;
	}

	public bool LookForWall() {
		wall = Physics2D.OverlapCircle(check.position, checkRadius, whatIsWall);
		return wall;
	}

	private void OnDrawGizmos()	{
		Gizmos.DrawWireSphere(check.position, checkRadius);
	}
}
