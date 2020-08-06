using UnityEngine;

public class FeatherPickup : MonoBehaviour {

    GameManager gameManager;

	GameObject parentObject;

	private void Start() {
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		parentObject = transform.parent.gameObject;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			gameManager.IncreaseScore(1);
			ObjectPooler.Instance.SpawnFromPool("PickupSoundEffect", transform);
			ObjectPooler.Instance.Despawn(parentObject);
		}
	}

}
