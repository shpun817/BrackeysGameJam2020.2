using UnityEngine;

public class ShieldPickup : MonoBehaviour {

    GameManager gameManager;

	private void Awake() {
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			gameManager.ShieldHeart();
			Destroy(gameObject);
		}
	}

}
