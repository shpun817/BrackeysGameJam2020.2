using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestoreHealthIndicator : MonoBehaviour {

    public GameManager gameManager;
	public Sprite[] sprites;

	int numSprites;
	Image imageComponent;
	int restoreHealthAmount;

	WaitUntil waitUntilRestoreHealthAmountInconsistent;

	Animator animator;

	private void Start() {
		imageComponent = GetComponent<Image>();
		if (!imageComponent) {
			Debug.LogWarning("Image component not found on Restore Health Indicator.");
		}
		if (!gameManager) {
			Debug.LogWarning("Game Manager not loaded on RestoreHealth Indicator!");
		}
		numSprites = sprites.Length;
		restoreHealthAmount = gameManager.GetRestoreHealthAmount();
		if (gameManager.GetMaxRestoreHealth() + 1 != numSprites) {
			Debug.LogWarning("Wrong number of sprites on Restore Health Indicator!");
		}
		waitUntilRestoreHealthAmountInconsistent = new WaitUntil(RestoreHealthAmountInconsistent);

		animator = GetComponent<Animator>();

		Setup();
	}

	public void Setup() {
		StartCoroutine(UpdateSprite());
	}

	IEnumerator UpdateSprite() {
		yield return waitUntilRestoreHealthAmountInconsistent;

		// Update restoreHealthAmount variable
		restoreHealthAmount = gameManager.GetRestoreHealthAmount();

		int determinant = restoreHealthAmount;

		// Choose the correct sprite according to restoreHealthAmount
		Sprite sprite = imageComponent.sprite;
		if (determinant <= 0) {
			sprite = sprites[0];
		} else if (determinant < numSprites) {
			sprite = sprites[determinant];
		} else {
			sprite = sprites[numSprites-1];
		}
		

		// Change the sprite of the image component
		imageComponent.sprite = sprite;

		StartCoroutine(UpdateSprite());
	}

	bool RestoreHealthAmountInconsistent() {
		int check = gameManager.GetRestoreHealthAmount() - restoreHealthAmount;

		if (check > 0) {
			animator.SetTrigger("Restore");
		}

		return (check != 0);
	}

}
