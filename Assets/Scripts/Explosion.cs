using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IDestroySelf, ISetup {
	
	Animator animator;

	private void Awake() {
		animator = GetComponent<Animator>();
	}

    public void DestroySelf() {
		gameObject.SetActive(false);
	}

	public void Setup() {
		if (animator) {
			animator.SetTrigger("StartExplosion");
		}
	}

}
