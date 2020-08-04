using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShieldIndicator : MonoBehaviour {

    public GameManager gameManager;

	bool isHeartShielded = false;
	WaitUntil waitUntilIsHeartShieldedInconsistent;
	Image imageComponent;
	Color imageColorOriginal;

	private void Awake() {
		if (!gameManager) {
			Debug.LogError("Game Manager not attached on Shield Indicator!");
		}
		imageComponent = GetComponent<Image>();
		if (!imageComponent) {
			Debug.LogError("Image component not found on Shield Indicator");
		}
		imageColorOriginal = new Color(1f, 1f, 1f, 0.85f);
		waitUntilIsHeartShieldedInconsistent = new WaitUntil(isHeartShieldedInconsistent);
		Setup();
	}

	public void Setup() {
		StartCoroutine(SetShieldSprite());
	}

	bool isHeartShieldedInconsistent() {
		return isHeartShielded != gameManager.GetIsHeartShielded();
	}

	IEnumerator SetShieldSprite() {
		yield return waitUntilIsHeartShieldedInconsistent;

		isHeartShielded = gameManager.GetIsHeartShielded();

		if (isHeartShielded) {
			imageComponent.color = imageColorOriginal;
		} else {
			imageComponent.color = Color.clear;
		}

		StartCoroutine(SetShieldSprite());
	}

}
