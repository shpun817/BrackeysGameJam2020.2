using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[SerializeField]
	int playerMaxHealth = 4;

	[SerializeField]
	int playerHealth;

	private void Awake() {
		Setup();
	}

	public void Setup() {
		playerHealth = playerMaxHealth;
	}

	public int GetPlayerHealth() {
		return playerHealth;
	}

	public int GetPlayerMaxHealth() {
		return playerMaxHealth;
	}


}
