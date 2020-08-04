using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	[SerializeField, Range(0, 10)]
	int playerMaxHealth = 4;

	[SerializeField]
	int playerHealth;

	[SerializeField, Range(1, 20)]
	int targetScore = 5;

	[SerializeField]
	int playerScore = 0;

	[SerializeField]
	float playerAltitudeLimit = -500f;

	[SerializeField]
	float flashTime = 1f;

	bool isPlayerFlashing = false;
	bool isHeartShielded = false;

	Transform playerTransform;
	Rigidbody2D playerRigidbody;
	WaitUntil waitUntilPlayerPositionTooLow;
	RewindTime playerRewindTime;
	Animator playerAnimator;
	WaitForSeconds waitForFlashTime;

	private void Awake() {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		playerTransform = player.GetComponent<Transform>();
		if (!playerTransform) {
			Debug.LogError("Player not found by Game Manager.");
		}
		playerRigidbody = player.GetComponent<Rigidbody2D>();
		if (!playerRigidbody) {
			Debug.LogError("Rigidbody2D not found on player.");
		}
		playerRewindTime = player.GetComponent<RewindTime>();
		if (!playerRigidbody) {
			Debug.LogError("RewindTime not found on player.");
		}
		playerAnimator = player.GetComponent<Animator>();
		if (!playerRigidbody) {
			Debug.LogError("Animator not found on player.");
		}
		waitUntilPlayerPositionTooLow = new WaitUntil(PlayerPositionTooLow);
		waitForFlashTime = new WaitForSeconds(flashTime);
		Setup();
	}

	public void Setup() {
		playerHealth = playerMaxHealth;
		playerScore = 0;

		StartCoroutine(TeleportPlayerToOrigin());
	}

	private void Update() {
		int gameOverCheck = GameOverCheck();
		if (gameOverCheck != 0) {
			if (gameOverCheck == -1) {
				// LOSE
				//Debug.Log("Player loses.");
				SceneManager.LoadScene("Lose");
			} else if (gameOverCheck == 1) {
				// WIN
				//Debug.Log("Player wins.");
				SceneManager.LoadScene("Win");
			} else {
				Debug.LogError("Game Over check receives invalid value.");
			}
		}
	}

	int GameOverCheck() {
		// Return -1 for losing, 1 for winning, 0 for not game over yet

		if (playerHealth <= 0) {
			return -1;
		}

		if (playerScore >= targetScore) {
			return 1;
		}

		return 0;
	}

	bool PlayerPositionTooLow() {
		return (playerTransform.position.y < playerAltitudeLimit);
	}

	IEnumerator TeleportPlayerToOrigin() {
		yield return waitUntilPlayerPositionTooLow;

		Vector3 offset = Vector3.zero - playerTransform.position;
		playerRewindTime.ApplyOffsetToStoredPositions(offset);
		playerTransform.position = Vector3.zero;


		StartCoroutine(TeleportPlayerToOrigin());
	}

	public int GetPlayerHealth() {
		return playerHealth;
	}

	public int GetPlayerMaxHealth() {
		return playerMaxHealth;
	}

	public bool GetIsHeartShielded() {
		return isHeartShielded;
	}

	public int GetPlayerScore() {
		return playerScore;
	}

	public int GetTargetScore() {
		return targetScore;
	}
	
	public void IncreaseScore(int increaseAmount) {
		playerScore += increaseAmount;
	}

	public void PlayerTakeDamage(int damageAmount) {

		if (isPlayerFlashing) {
			return;
		}

		isPlayerFlashing = true;

		// Play sound

		// Play animation
		playerAnimator.SetTrigger("StartHurt");
		StartCoroutine(StopHurt());

		if (isHeartShielded) {
			isHeartShielded = false;
		} else {
			playerHealth -= damageAmount;
		}
	}

	IEnumerator StopHurt() {
		yield return waitForFlashTime;

		playerAnimator.SetTrigger("EndHurt");
		isPlayerFlashing = false;
	}

	public void ShieldHeart() {
		isHeartShielded = true;
	}


}
