using UnityEngine;

public class RandomizeAnimatorSpeedOnAwake : MonoBehaviour {

	public float minSpeed = 0.5f;
	public float maxSpeed = 1.5f;

	private void Awake() {
		Animator animator = GetComponent<Animator>();
		if (animator) {
			animator.speed = Random.Range(minSpeed, maxSpeed);
		}
	}

}
