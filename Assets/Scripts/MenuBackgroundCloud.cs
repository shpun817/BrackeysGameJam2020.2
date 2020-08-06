using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackgroundCloud : MonoBehaviour {


    float riseSpeed;

	RectTransform rectTransform;

	float myX;

	private void Awake() {
		rectTransform = GetComponent<RectTransform>();
		if (rectTransform) {
			myX = rectTransform.anchoredPosition.x;
		}
		riseSpeed = Random.Range(30f, 60f);
	}

	private void FixedUpdate() {
		if (rectTransform) {
			float newY = rectTransform.anchoredPosition.y + riseSpeed * Time.fixedDeltaTime;
			rectTransform.anchoredPosition = new Vector3(myX, newY);
			if (newY >= 349f) {
				rectTransform.anchoredPosition = new Vector3(myX, -350f);
				riseSpeed = Random.Range(30f, 60f);
			}
		}
	}

}
