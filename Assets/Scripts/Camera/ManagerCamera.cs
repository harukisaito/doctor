using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerCamera : MonoBehaviour {

	private Camera managerCamera;
	private Image whiteImage;
	private AudioListener audioListener;

	public static ManagerCamera Instance;

	private void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}

	private void Start() {
		managerCamera = GetComponent<Camera>();
		audioListener = GetComponent<AudioListener>();
		whiteImage = GetComponentInChildren<Image>();
	}

	public void OnFinishedLoadingStartMenu(object src, EventArgs e) {
		ActivateCamera(false);
	}

	public void OnFinishedLoadingLevel(object src, EventArgs e) {
		ActivateCamera(false);
	}

	public void OnFinishAnimation(object src, EventArgs e) {
		ActivateCamera(true);
	}

	public void OnFinishedLoadingEndMenu(object src, EventArgs e) {
		ActivateCamera(false);
	}

	public void ActivateCamera(bool enabled) {
		managerCamera.enabled = enabled;
		whiteImage.enabled = enabled;
		audioListener.enabled = enabled; 
	}
}
