using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonPlant : MonoBehaviour {

	private void OnCollisionEnter2D(Collision2D other) {
		GameObject obj = other.gameObject;
		if (obj.tag == "Player") {
			Debug.Log("Plant hit Player!");
		}
	}

}
