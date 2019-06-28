using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[SerializeField] private KeyCode pauseKey;

	private Player player;
	private Coroutine coroutine;
	private WaitForSeconds second = new WaitForSeconds(1f);

	public EventHandler Pause;
	public EventHandler UnPause;

	private bool paused = false;

	public float Timer {get; private set;}
	public Player Player {
		get { return player; }
		set { player = value; }
	}

	public static GameManager Instance;


	private void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}

	private void Update() {
		if(Input.GetKeyDown(pauseKey) && !paused) {
			Time.timeScale = 0;
			paused = true;
			OnPause();
		} else 
		if(Input.GetKeyDown(pauseKey) && paused) {
			Time.timeScale = 1;
			paused = false;
			OnUnPause();
		}
	}

	public void OnFinishedLoadingLevel(object src, EventArgs e) {
		coroutine = StartCoroutine(StartTimer());
	}

	public void OnFinish(object src, EventArgs e) {
		StopCoroutine(coroutine);
	}

	protected virtual void OnPause() {
		if(Pause != null) {
			Pause(this, EventArgs.Empty);
		}
	}
	protected virtual void OnUnPause() {
		if(UnPause != null) {
			UnPause(this, EventArgs.Empty);
		}
	}

	private IEnumerator StartTimer() {
		while(true) {
			Timer ++;
			yield return second;
		}
	}
}
