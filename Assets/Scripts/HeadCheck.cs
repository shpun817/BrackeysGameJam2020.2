using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCheck : MonoBehaviour {

	CharacterControl player;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl>();
		if (!player) {
			Debug.LogError("Player Character Control not found by Player Head.");
		}
	}

    // Handle bouncing off enemies
	private void OnCollisionEnter2D(Collision2D other) {
		GameObject obj = other.gameObject;
		if (obj.tag == "Enemy") {
			player.Bounce();
		}
	}

}
