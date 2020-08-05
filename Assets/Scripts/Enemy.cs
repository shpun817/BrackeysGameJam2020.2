using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ISetup {

	public float stunDuration = 3f;

	bool isFacingRight = true;

	Transform player;

	bool isStunned = false;
	WaitForSeconds waitForStunDuration;

	public void Setup() {

		player = GameManager.Player.transform;
		waitForStunDuration = new WaitForSeconds(stunDuration);

		// Face the player (assuming the enemy sprite faces right by default)
		if (transform.position.x > player.position.x) { // self is on the right of player
			isFacingRight = false;
		} else {
			isFacingRight = true;
		}

		if (!isFacingRight) {
			// Flip the sprite horizontally (x-axis)
			transform.localScale = new Vector3(-1, 1, 1);
		} else {
			transform.localScale = Vector3.one;
		}

	}

	/* Not used
    private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			Debug.Log("Player hit!");
		}
	}
	*/

	public void Stun() {
		isStunned = true;
		StartCoroutine(UnStun());
	}

	IEnumerator UnStun() {
		yield return waitForStunDuration;
		isStunned = false;
	}

	public bool GetIsStunned() {
		return isStunned;
	}

	public bool GetIsFacingRight() {
		return isFacingRight;
	}

}
