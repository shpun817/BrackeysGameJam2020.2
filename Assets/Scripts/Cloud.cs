using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour, ISetup {

    public void Setup() {

		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer) {
			spriteRenderer.color = new Color(1f, 1f, 1f, Random.Range(0.2f, 0.67f));
		}

	}

}
