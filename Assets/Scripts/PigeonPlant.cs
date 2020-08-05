using UnityEngine;

public class PigeonPlant : MonoBehaviour {

	public int damage = 1;

	GameManager gameManager;
	CharacterControl player;

	private void Start() {
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		if (!gameManager) {
			Debug.LogWarning("Game Manager not found by " + gameObject.name);
		}
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl>();
		if (!player) {
			Debug.LogWarning("Player not found by " + gameObject.name);
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		GameObject obj = other.gameObject;
		if (obj.tag == "Player") {
			//Debug.Log("Plant hit Player!");
			gameManager.PlayerTakeDamage(damage);
			player.Bounce();
		}
	}

}
