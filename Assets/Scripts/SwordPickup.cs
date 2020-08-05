using UnityEngine;

public class SwordPickup : MonoBehaviour {

    GameManager gameManager;

	GameObject parentObject;

	private void Start() {
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		parentObject = transform.parent.gameObject;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			gameManager.EquipSword();
			ObjectPooler.Instance.Despawn(parentObject);
		}
	}

}
