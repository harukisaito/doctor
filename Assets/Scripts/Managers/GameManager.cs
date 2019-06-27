using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private Player player;

	public float Timer {get; private set;}
	private Coroutine coroutine;
	private WaitForSeconds second = new WaitForSeconds(1f);

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

	public void OnFinishedLoadingLevel(object src, EventArgs e) {
		coroutine = StartCoroutine(StartTimer());
	}

	public void OnFinish(object src, EventArgs e) {
		StopCoroutine(coroutine);
	}

	private IEnumerator StartTimer() {
		while(true) {
			Timer ++;
			yield return second;
		}
	}
}
