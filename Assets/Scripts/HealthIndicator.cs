using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour {

    public GameManager gameManager;
	public Sprite[] sprites;

	int numSprites;
	Image imageComponent;
	int playerHealth;

	WaitUntil waitUntilPlayerHealthInconsistent;

	private void Awake() {
		imageComponent = GetComponent<Image>();
		if (!imageComponent) {
			Debug.LogError("Image component not found on Health Indicator.");
		}
		if (!gameManager) {
			Debug.LogError("Game Manager not loaded on Health Indicator!");
		}
		numSprites = sprites.Length;
		playerHealth = gameManager.GetPlayerMaxHealth();
		if ((playerHealth + 1) != numSprites) {
			Debug.LogError("Wrong number of sprites on Health Indicator!");
		}
		waitUntilPlayerHealthInconsistent = new WaitUntil(PlayerHealthInconsistent);

		Setup();
	}

	public void Setup() {
		StartCoroutine(UpdateSprite());
	}

	IEnumerator UpdateSprite() {
		yield return waitUntilPlayerHealthInconsistent;

		// Update playerHealth variable
		playerHealth = gameManager.GetPlayerHealth();

		// Choose the correct sprite according to playerHealth
		Sprite sprite = imageComponent.sprite;
		if (playerHealth <= 0) {
			sprite = sprites[0];
		} else if (playerHealth < numSprites) {
			sprite = sprites[playerHealth];
		} else {
			sprite = sprites[numSprites-1];
		}
		

		// Change the sprite of the image component
		imageComponent.sprite = sprite;

		StartCoroutine(UpdateSprite());
	}

	bool PlayerHealthInconsistent() {
		return (playerHealth != gameManager.GetPlayerHealth());
	}

}
