using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	public static Goal Instance;

	private MovementInputController movementInputController;

	public EventHandler Finish;
	public EventHandler FinishAnimation;

	private void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}


	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			movementInputController = GameManager.Instance.Player.GetComponent<MovementInputController>();
			movementInputController.EnableInput = false;
			AudioManager.Instance.Play("Finish");
			OnFinish();
			StartCoroutine(EndOfAnimation());
		}
	}

	private IEnumerator EndOfAnimation() {
		yield return new WaitForSeconds(5f);
		OnFinishAnimation();
	}

	protected virtual void OnFinish() {
		if(Finish != null) {
			Finish(this, EventArgs.Empty);
		}
	}

	protected virtual void OnFinishAnimation() {
		SceneManagement.Instance.LoadScene(Scenes.EndMenu, true);
		if(FinishAnimation != null) {
			FinishAnimation(this, EventArgs.Empty);
		}
	}
}
