using UnityEngine;

public class ShieldPickup : MonoBehaviour {

    GameManager gameManager;

	private void Start() {
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			gameManager.ShieldHeart();
			ObjectPooler.Instance.SpawnFromPool("PickupSoundEffect", transform);
			ObjectPooler.Instance.Despawn(gameObject);
		}
	}

}
