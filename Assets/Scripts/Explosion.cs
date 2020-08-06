using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IDestroySelf, ISetup {
	
	Animator animator;

	AudioSource audioSource;

	private void Awake() {
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

    public void DestroySelf() {
		gameObject.SetActive(false);
	}

	public void Setup() {
		if (animator) {
			animator.SetTrigger("StartExplosion");
		}
		if (audioSource) {
			audioSource.Play();
		}
	}

}
