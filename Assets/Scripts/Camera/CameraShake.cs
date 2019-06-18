using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	[SerializeField] private float magnitude;

	public void OnDashStart(object src, EventArgs e) {
        StartCoroutine(Shake());
	}

	private IEnumerator Shake() {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0f;

        while(elapsed < 0.1) {
            float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            
            transform.localPosition = new Vector2(x, y);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
