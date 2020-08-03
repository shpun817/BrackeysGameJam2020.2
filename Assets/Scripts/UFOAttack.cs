using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOAttack : MonoBehaviour {

	public Transform[] attackPositions;
	public float cooldown = 3.5f;
	public GameObject projectileObj;
	public float probabilityToAttack = 0.6f;

	WaitForSeconds waitForCoolDown;
	int numAttackPositions;

    private void Awake() {
		waitForCoolDown = new WaitForSeconds(cooldown);
		numAttackPositions = attackPositions.Length;
		Setup();
	}

	public void Setup() {
		StartCoroutine("PrepareAttack");
	}

	IEnumerator PrepareAttack() {

		yield return waitForCoolDown;

		Attack();

		StartCoroutine("PrepareAttack");
	}

	void Attack() {

		// For each attack position, roll dice to determine if a projectile should be spawned there
		for (int i = 0; i < numAttackPositions; ++i) {
			if (Random.Range(0f, 1f) <= probabilityToAttack) {
				Projectile projectile = Instantiate(projectileObj, attackPositions[i].position, attackPositions[i].rotation).GetComponent<Projectile>();
				if (projectile) {
					projectile.Setup();
				}
			}
		}

	}

}
