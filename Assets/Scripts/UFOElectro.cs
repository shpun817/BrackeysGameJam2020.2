using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOElectro : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D other) {
		GameObject obj = other.gameObject;
		if (obj.tag == "Player") {
			Debug.Log("Electro hit Player!");
		}
	}

}
