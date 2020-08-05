using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WingIndicator : MonoBehaviour {

    public GameManager gameManager;
	public Sprite[] sprites;

	int numSprites;
	Image imageComponent;
	int playerScore;

	WaitUntil waitUntilPlayerScoreInconsistent;

	private void Start() {
		imageComponent = GetComponent<Image>();
		if (!imageComponent) {
			Debug.LogWarning("Image component not found on Wing Indicator.");
		}
		if (!gameManager) {
			Debug.LogWarning("Game Manager not loaded on Wing Indicator!");
		}
		numSprites = sprites.Length;
		playerScore = gameManager.GetPlayerScore();
		if (gameManager.GetTargetScore() != numSprites) {
			Debug.LogWarning("Wrong number of sprites on Wing Indicator!");
		}
		waitUntilPlayerScoreInconsistent = new WaitUntil(PlayerScoreInconsistent);

		Setup();
	}

	public void Setup() {
		StartCoroutine(UpdateSprite());
	}

	IEnumerator UpdateSprite() {
		yield return waitUntilPlayerScoreInconsistent;

		// Update playerScore variable
		playerScore = gameManager.GetPlayerScore();

		// Choose the correct sprite according to playerHealth
		Sprite sprite = imageComponent.sprite;
		if (playerScore <= 0) {
			sprite = sprites[0];
		} else if (playerScore < numSprites) {
			sprite = sprites[playerScore];
		} else {
			sprite = sprites[numSprites-1];
		}
		

		// Change the sprite of the image component
		imageComponent.sprite = sprite;

		StartCoroutine(UpdateSprite());
	}

	bool PlayerScoreInconsistent() {
		return (playerScore != gameManager.GetPlayerScore());
	}

}
