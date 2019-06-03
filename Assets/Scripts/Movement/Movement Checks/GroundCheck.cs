using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MovementController))]
public class GroundCheck : MonoBehaviour {

	[SerializeField] private Transform groundCheck;
	[SerializeField] private float checkRadius = 0.1f;	
	[SerializeField] private LayerMask whatIsGround; 

	private MovementController movementController;
	private SpriteRenderer[] spriteRenderers;
	private SpriteRenderer dropShadow;

	[SerializeField] private bool touchingGround;
	[SerializeField] private bool isGrounded;
	[SerializeField] private bool onPlatform;

	public bool  OnPlatform {
		get { return onPlatform;}
	 	set { print("VALUE = " + value);
			 onPlatform = value;}
	}
	public bool IsGrounded {
		get {return isGrounded;}
	}

	public bool IsGroundedAnimation {
		get {return touchingGround;}
	}


	private void Awake() {
		GetComponents();
	}

	private void FixedUpdate() {
		touchingGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
		if(gameObject.tag == "Player") {
			// print("ON PLATFORM = " + onPlatform);
			// print("TOUCHING THE GROUND = " + touchingGround);

		}

		if(onPlatform) { // has touched a platform
			if(touchingGround) { // physicsCheck has touched the ground
				// if(onPlatform) { // collider on platform recognizes that the player is on the platform
					ResetPlayerAbilites();

					if(gameObject.tag == "Player") {
						// print("IS GROUNDED");
					}	
				// }
				// else isGrounded = false;
			}
			else isGrounded = false;
		}
		else { // is not on a platform
			if(touchingGround) { // physicsCheck has touched the ground
				ResetPlayerAbilites();
				if(gameObject.tag == "Player") {
					// print("IS GROUNDED");
				}
			}
			else isGrounded = false;
		}
		if(gameObject.CompareTag("Player")) {
			if(!touchingGround) {
				dropShadow.enabled = false;
			}
		}
	}

	private void GetComponents() {
		movementController = GetComponent<MovementController>();
		if(gameObject.CompareTag("Player")) {
			spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
			foreach(var sR in spriteRenderers) {
				if(sR.gameObject.CompareTag("Drop Shadow")) {
					dropShadow = sR;
					dropShadow.enabled = false;
				}
			}
		}
	}

	private void ResetPlayerAbilites() {
		if(gameObject.CompareTag("Player")) {
			dropShadow.enabled = true;
		}
		isGrounded = true;
		movementController.IsStomping = false;
		movementController.DoubleJump = true;
		movementController.Dashed = false;
		if(!movementController.DisableKnockback) {
			movementController.KnockedBack = false;
		}
	}
}
