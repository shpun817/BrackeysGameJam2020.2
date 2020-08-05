using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeAngelAttack : MonoBehaviour, ISetup {

	public float cooldown = 2f;

	//public GameObject projectileObj;

	WaitForSeconds waitForCoolDown;
	WaitWhile waitWhileStunned;
	Transform selfTransform;
	Enemy enemyComponent;

	private void Awake() {
		/*
		if (!projectileObj) {
			Debug.LogWarning("No projectile object loaded on Office Angel Attack!");
		}
		*/
		waitForCoolDown = new WaitForSeconds(cooldown);
		selfTransform = GetComponent<Transform>();
		enemyComponent = GetComponent<Enemy>();
		if (!enemyComponent) {
			Debug.LogWarning("Enemy component not found on OfficeAngel!");
		}
		waitWhileStunned = new WaitWhile(enemyComponent.GetIsStunned);
	}

	public void Setup() {
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
		Projectile projectile = ObjectPooler.Instance.SpawnFromPool("OfficeAngelProjectile", selfTransform.position, selfTransform.rotation).GetComponent<Projectile>();
		if (projectile) {
			projectile.Setup();
		}

	}

}
