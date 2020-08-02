using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float stunDuration = 3f;

	bool isFacingRight = true;

	Transform player;
	Transform selfTransform;

	bool isStunned = false;
	WaitForSeconds waitForStunDuration;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		if (!player) {
			Debug.LogError("Player Transform not found by " + gameObject.name);
		}
		selfTransform = GetComponent<Transform>();
		waitForStunDuration = new WaitForSeconds(stunDuration);
		Setup();
	}

	public void Setup() {

		// Face the player (assuming the enemy sprite faces right by default)
		if (selfTransform.position.x > player.position.x) { // self is on the right of player
			isFacingRight = false;
		} else {
			isFacingRight = true;
		}

		if (!isFacingRight) {
			// Flip the sprite horizontally (x-axis)
			selfTransform.localScale = new Vector3(-1, 1, 1);
		} else {
			selfTransform.localScale = Vector3.one;
		}

	}

    private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			Debug.Log("Player hit!");
		}
	}

	public IEnumerator Stun() {
		isStunned = true;
		yield return waitForStunDuration;
	}

	void UnStun() {
		isStunned = false;
	}

	public bool GetIsStunned() {
		return isStunned;
	}

}
