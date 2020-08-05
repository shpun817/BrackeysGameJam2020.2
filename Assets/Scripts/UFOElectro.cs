using UnityEngine;

public class UFOElectro : MonoBehaviour {

	public int damage = 1;

	GameManager gameManager;

	private void Start() {
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		if (!gameManager) {
			Debug.LogWarning("Game Manager not found by " + gameObject.name);
		}
	}

    private void OnCollisionEnter2D(Collision2D other) {
		GameObject obj = other.gameObject;
		if (obj.tag == "Player") {
			//Debug.Log("Electro hit Player!");
			gameManager.PlayerTakeDamage(damage);
		}
	}

}
