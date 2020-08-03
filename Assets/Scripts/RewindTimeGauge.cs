using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindTimeGauge : MonoBehaviour {

	public RewindTime rewindTime;

	public Sprite[] sprites;

	int numSprites;
	float interval;

	Image imageComponent;

	private void Awake() {
		imageComponent = GetComponent<Image>();
		if (!imageComponent) {
			Debug.LogError("Image component not found on Rewind Time Gauge.");
		}

		Setup();
	}

	public void Setup() {
		numSprites = sprites.Length;
		interval = 1f / numSprites;
	}

    void FixedUpdate() {
        float rewindMeter = rewindTime.GetRewindMeter();

		//Debug.Log(rewindMeter);

		// Determine the target sprite index
		float determinant = 1f;
		int spriteIndex = numSprites - 1;
		while (determinant >= 0f) {
			
			if (determinant <= rewindMeter) {
				break;
			}

			--spriteIndex;
			determinant -= interval;
		}

		//Debug.Log(determinant);

		if (determinant < 0f || spriteIndex < 0) {
			spriteIndex = 0;
		}

		// Change the sprite of the image component according to the sprite index
		imageComponent.sprite = sprites[spriteIndex];

		// Make the sprite opaque if rewind is in cooldown
		imageComponent.color = rewindTime.GetIsInCooldown()? (new Color(1f, 1f, 1f, 0.5f)) : (new Color(1f, 1f, 1f, 1f));
    }

}
