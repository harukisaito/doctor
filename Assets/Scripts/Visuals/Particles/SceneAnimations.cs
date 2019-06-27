using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAnimations : MonoBehaviour {

	private Animator animator;

	private void Start() {
		animator = GetComponent<Animator>();
	}
	public void FadeOut() {
		if(animator == null) {
			animator = GetComponent<Animator>();
		}
		
		animator.SetTrigger("SceneChange");
	}
}
