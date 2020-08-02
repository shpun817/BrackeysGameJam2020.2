using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectWhenTooFarFromPlayer : MonoBehaviour {

	float maxDistance = 15f;

	Transform player;
	Transform selfTransform;

    private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		if (!player) {
			Debug.LogError("Player Transform not found by " + gameObject.name);
		}
		selfTransform = GetComponent<Transform>();
	}

    void FixedUpdate() {
        float distance = Mathf.Abs((player.position - selfTransform.position).magnitude);

		if (distance > maxDistance) {
			Destroy(gameObject);
		}
    }

}
