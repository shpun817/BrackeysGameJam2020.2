using UnityEngine;

public class Projectile : MonoBehaviour, IDestroySelf, ISetup {

	public float moveSpeed = 6.5f;

	public float noiseSize = 0.4f;

	public int damage = 1;

	Transform playerTransform;
	Rigidbody2D selfRigidbody;
	Vector3 direction;
	GameManager gameManager;

	string goingForTag = "Player";

	public void Setup() {

		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		if (!gameManager) {
			Debug.LogWarning("Game Manager not found by " + gameObject.name);
		}
		playerTransform = GameManager.Player.transform;
		if (!playerTransform) {
			Debug.LogWarning("Player not found by Projectile " + gameObject.name);
		}
		selfRigidbody = GetComponent<Rigidbody2D>();
		if (!selfRigidbody) {
			Debug.LogWarning("Rigidbody2D not found on " + gameObject.name);
		}

		//Debug.Log("Projectile Setup() called on " + gameObject.name);
		// Determine the movement direction
		direction = (playerTransform.position - transform.position).normalized;
		direction.z = 0f; // Make sure the z component is zero

		// Introduce noises to the x and y components
		direction.x += Random.Range(-noiseSize, noiseSize);
		direction.y += Random.Range(-noiseSize, noiseSize);

		direction = direction.normalized;

		GoInDirection();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" || other.tag == "Enemy") {
			//Debug.Log("Projectile hit player or enemy!");
			if (other.tag == "Player" && goingForTag == "Player") {

				gameManager.PlayerTakeDamage(damage);
				DestroySelf();

			} else if (other.tag == "Enemy" && goingForTag == "Enemy") {

				DestructibleEnemy destructibleEnemy = other.GetComponent<DestructibleEnemy>();

				if (destructibleEnemy) {
					destructibleEnemy.DestroySelf();
				}

				DestroySelf();

			}
			
		} else if (other.tag == "Deflector") {
			//Debug.Log("Projectile hit deflector!");
			goingForTag = "Enemy";

			direction = (transform.position - other.transform.position).normalized;

			moveSpeed *= 1.5f;

			GoInDirection();
		}
	}

	void GoInDirection() {
		transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);

		selfRigidbody.velocity = direction * moveSpeed;
	}

	public void DestroySelf() {

		gameObject.SetActive(false);

		// Play visuals
		ObjectPooler.Instance.SpawnFromPool("ProjectileExplosion", transform);

		// Play audio

	}

}
