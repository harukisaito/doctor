using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAnimations : MonoBehaviour {

	private Animator animator;

	private bool done;

	private void Start() {
		animator = GetComponent<Animator>();
	}

	// private void Update() {
	// 	if(SceneManagement.Instance.ChangingScene) {
	// 		if(!done) {
	// 			FadeOut();
	// 			done = true;
	// 		}
	// 	}
	// }

	public void FadeOut() {
		if(animator == null) {
			animator = GetComponent<Animator>();
		}
		animator.SetTrigger("SceneChange");
		print("changing scene");
	}
}
