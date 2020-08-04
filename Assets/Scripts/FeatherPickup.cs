using UnityEngine;

public class FeatherPickup : MonoBehaviour {

    GameManager gameManager;

	private void Awake() {
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			gameManager.IncreaseScore(1);
			Destroy(gameObject);
		}
	}

}
