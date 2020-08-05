using UnityEngine;

public class Projectile : MonoBehaviour, IDestroySelf {

	public float moveSpeed = 6.5f;

	public float noiseSize = 0.4f;

	public int damage = 1;

	Transform playerTransform;
	Transform selfTransform;
	Rigidbody2D selfRigidbody;
	Vector3 direction;
	GameManager gameManager;

	string goingForTag = "Player";

	private void Start() {
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		if (!gameManager) {
			Debug.LogWarning("Game Manager not found by " + gameObject.name);
		}
		playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		if (!playerTransform) {
			Debug.LogWarning("Player not found by Projectile " + gameObject.name);
		}
		selfRigidbody = GetComponent<Rigidbody2D>();
		if (!selfRigidbody) {
			Debug.LogWarning("Rigidbody2D not found on " + gameObject.name);
		}
		selfTransform = GetComponent<Transform>();
	}

	public void Setup() {
		//Debug.Log("Projectile Setup() called on " + gameObject.name);
		// Determine the movement direction
		direction = (playerTransform.position - selfTransform.position).normalized;
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
			direction = -direction;

			GoInDirection();
		}
	}

	void GoInDirection() {
		selfTransform.rotation = Quaternion.FromToRotation(Vector3.right, direction);

		selfRigidbody.velocity = direction * moveSpeed;
	}

	public void DestroySelf() {

		gameObject.SetActive(false);

		// Play visuals

		// Play audio

	}

}
