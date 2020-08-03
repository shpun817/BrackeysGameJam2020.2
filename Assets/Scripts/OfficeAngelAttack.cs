using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeAngelAttack : MonoBehaviour {

	public float cooldown = 2f;

	public GameObject projectileObj;

	WaitForSeconds waitForCoolDown;
	WaitWhile waitWhileStunned;
	Transform selfTransform;
	Enemy enemyComponent;

	private void Awake() {
		if (!projectileObj) {
			Debug.LogError("No projectile object loaded on Office Angel Attack!");
		}
		waitForCoolDown = new WaitForSeconds(cooldown);
		selfTransform = GetComponent<Transform>();
		enemyComponent = GetComponent<Enemy>();
		if (!enemyComponent) {
			Debug.LogError("Enemy component not found on OfficeAngel!");
		}
		waitWhileStunned = new WaitWhile(enemyComponent.GetIsStunned);
		Setup();
	}

	void Setup() {
		StartCoroutine("PrepareAttack");
	}

	IEnumerator PrepareAttack() {

		yield return waitWhileStunned;

		Attack();

		yield return waitForCoolDown;
		StartCoroutine("PrepareAttack");
	}

	void Attack() {

		// Shoot projectile in the specified direction
		// Will be optimized by object pooling
		Projectile projectile = Instantiate(projectileObj, selfTransform.position, selfTransform.rotation).GetComponent<Projectile>();
		if (projectile) {
			projectile.Setup();
		}

	}

}
