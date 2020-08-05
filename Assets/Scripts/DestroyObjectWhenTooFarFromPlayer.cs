using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectWhenTooFarFromPlayer : MonoBehaviour {

	float maxDistance = 70f;

	Transform player;
	Transform selfTransform;

	readonly WaitForSeconds waitFor5Seconds = new WaitForSeconds(5f);

    private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		if (!player) {
			Debug.LogWarning("Player Transform not found by " + gameObject.name);
		}
		selfTransform = GetComponent<Transform>();
		StartCoroutine(distanceCheck());
	}

    IEnumerator distanceCheck() {
		yield return waitFor5Seconds;
        float distance = Mathf.Abs((player.position - selfTransform.position).magnitude);

		if (distance > maxDistance) {
			ObjectPooler.Instance.Despawn(gameObject);
		}
		StartCoroutine(distanceCheck());
    }

}
