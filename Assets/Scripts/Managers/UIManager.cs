using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	// [SerializeField] private GameObject transitionImage;
	[SerializeField] private Text time;
	[SerializeField] private Text hp;

	private Vector3 hpScale;
	private Vector2 originalPos;

	private Image healthBar;

	private float oneUnit;

	public static UIManager Instance;

	private void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}

	public void StartLevel() {
		SceneManagement.Instance.LoadScene(Scenes.LevelSakura, true);
		ManagerCamera.Instance.ActivateCamera(true);
		AudioManager.Instance.Play("UI Click");
	}

	public void Quit() {
		AudioManager.Instance.Play("UI Click");
		Application.Quit();
	}

	public void OnFinishedLoadingEndMenu(object src, EventArgs e) {
		DisplayTime();
		hp.text = GameManager.Instance.Player.HP.ToString();
	}

	public void OnPlayerDeath(object src, EventArgs e) {
		UpdateHealthBar();
	}

	public void OnPlayerDamage(object src, EventArgs e) {
		UpdateHealthBar();
	}

	private void DisplayTime() {
		float tempTime = GameManager.Instance.Timer;
		float seconds = tempTime % 60;
		int minutes = (int)tempTime / 60;
		string stringSeconds;
		string stringMinutes;

		if(minutes < 1) {
			stringMinutes = "00";
		} else 
		if(minutes < 10) {
			stringMinutes = "0" + minutes.ToString();
		}
		else {
			stringMinutes = minutes.ToString();
		}
		if(seconds < 1) {
			stringSeconds = "00"; 
		} else
		if(seconds < 10) {
			stringSeconds = "0" + seconds.ToString();
		}
		else {
			stringSeconds = seconds.ToString();
		}
		time.text = stringMinutes + ":" + stringSeconds;
	}

	private void UpdateHealthBar() {
		int playerHP = GameManager.Instance.Player.HP;
		hpScale = new Vector3(playerHP, healthBar.rectTransform.localScale.y, 0);
		healthBar.rectTransform.localScale = hpScale;
		healthBar.rectTransform.localPosition = originalPos + new Vector2((oneUnit / 2) * (-5 + playerHP), healthBar.rectTransform.localPosition.y);
	}

	public void OnFinishedLoadingLevel(object src, EventArgs e) {
		healthBar = HealthBar.Instance.GetComponent<Image>();

		originalPos = healthBar.rectTransform.localPosition;
		oneUnit = 100;
	}

}
