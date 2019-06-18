using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRender : MonoBehaviour {

	[SerializeField] private TrailRenderer dashTrail;
	private Color dashColor;
	private Coroutine dashFade;
	private Vector2 trailPosition;
	private float fadeValue = 1f;

	private MovementController movementController;

	private void Start() {
		movementController = GetComponent<MovementController>();
		trailPosition = dashTrail.gameObject.transform.localPosition;

		dashTrail.enabled = false;
		dashColor = dashTrail.startColor;
	}

	private void Update() {
		if(movementController.MovementDirection > 0) {
			dashTrail.gameObject.transform.localPosition = trailPosition;
		}
		else {
			dashTrail.gameObject.transform.localPosition = new Vector2(-trailPosition.x, trailPosition.y);
		}
	}

	public void OnDashStart(object src, EventArgs e) {
		EnableTrailRenderer(TrailRenderers.Dash);
	}

	public void OnDashEnd(object src, EventArgs e) {
		dashTrail.enabled = false;
		// dashFade = StartCoroutine(FadeOut(dashTrail));
	}

	public void OnPlayerDeath(object src, EventArgs e) {
		if(dashFade != null) {
			StopAllCoroutines();
		}
		dashTrail.enabled = false;
		dashTrail.Clear();
	}


	private IEnumerator FadeOut(TrailRenderer trailRenderer) {
		fadeValue = 1f;
		Color color = trailRenderer.startColor;
		while(fadeValue >= 0) {
			fadeValue -= Time.deltaTime * 2;

			trailRenderer.startColor
				= new Color(
					color.r,
					color.g,
					color.b,
					fadeValue
				);
			yield return null;
		}
		trailRenderer.enabled = false;
	}

	private void EnableTrailRenderer(TrailRenderers type) {
		EnableTrailRenderer(dashTrail, dashColor);
	}

	private void EnableTrailRenderer(TrailRenderer trailRenderer, Color color) {
		trailRenderer.startColor = color;
		trailRenderer.enabled = true;
		// if(other != null) {
			// StopCoroutine(other);
			
		// }
	}
}
