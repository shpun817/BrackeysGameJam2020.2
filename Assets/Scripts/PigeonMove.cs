using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonMove : MonoBehaviour {

	public float moveSpeed = 1.5f;

	Enemy enemyComponent;
	int direction = 1;
	Rigidbody2D selfRigidbody;

	WaitUntil waitUntilDirectionChanged;

	private void Awake() {
		enemyComponent = GetComponent<Enemy>();
		if (!enemyComponent) {
			Debug.LogError("Enemy Component not found on " + gameObject.name);
		}
		selfRigidbody = GetComponent<Rigidbody2D>();
		if (!selfRigidbody) {
			Debug.LogError("Rigidbody2D not found on " + gameObject.name);
		}
		waitUntilDirectionChanged = new WaitUntil(directionChanged);
		Setup();
	}

	public void Setup() {
		StartCoroutine("ChangeDirection");
	}

	IEnumerator ChangeDirection() {
		yield return waitUntilDirectionChanged;
		if (enemyComponent.GetIsFacingRight()) {
			direction = 1;
		} else {
			direction = -1;
		}
		//StartCoroutine("ChangeDirection");
	}

	bool directionChanged() {
		return enemyComponent.GetIsFacingRight() != (direction == 1);
	}

	private void FixedUpdate() {
		selfRigidbody.velocity = new Vector2(moveSpeed * direction, 0);
	}

}
