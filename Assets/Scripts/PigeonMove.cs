using System.Collections;
using UnityEngine;

public class PigeonMove : MonoBehaviour, ISetup {

	public float moveSpeed = 1.5f;

	Enemy enemyComponent;
	Rigidbody2D selfRigidbody;


	public void Setup() {

		enemyComponent = GetComponent<Enemy>();
		if (!enemyComponent) {
			Debug.LogWarning("Enemy Component not found on " + gameObject.name);
		}
		selfRigidbody = GetComponent<Rigidbody2D>();
		if (!selfRigidbody) {
			Debug.LogWarning("Rigidbody2D not found on " + gameObject.name);
		}

		Vector3 direction = (GameManager.Player.transform.position - transform.position).normalized;

		selfRigidbody.velocity = (new Vector2(direction.x, direction.y)) * moveSpeed;

	}

}
