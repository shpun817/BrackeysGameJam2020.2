using UnityEngine;

public class FeatherPickup : MonoBehaviour {

    GameManager gameManager;

	GameObject parentObject;

	private void Start() {
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		parentObject = GetComponent<Transform>().parent.gameObject;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			gameManager.IncreaseScore(1);
			ObjectPooler.Instance.Despawn(parentObject);
		}
	}

}
