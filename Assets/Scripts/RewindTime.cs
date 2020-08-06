using System.Collections;
using UnityEngine;

public class RewindTime : MonoBehaviour {

	public class Information {
		public Vector3 position;
		public Vector3 velocity;
		public Information(Vector3 p, Vector3 v) {
			position = p;
			velocity = v;
		}
		public static Information operator+(Information a, Vector3 positionOffset) {
			return new Information(a.position + positionOffset, a.velocity);
		}
	}

	public float maxSeconds = 3f;
	public float rewindCoolDown = 0.35f;

	CircularStackVector3 storedInformation;
	public Rigidbody2D playerRigidbody;
	CharacterControl playerController;
	bool isRewindPressed = false;
	bool isRewinding = false;
	bool isInCooldown = false;
	int maxSize;

	[SerializeField]
	string RewindParticleName = "RewindParticle";
	Quaternion particleRotation = Quaternion.Euler(0f, 0f, 0f);

	WaitForSeconds cooldown;
	WaitUntil releaseButtonClear;

	AudioSource audioSource;

    private void Awake() {
		if (maxSeconds <= 0) {
			Debug.LogWarning("Invalid input to Max Seconds in RewindTime.");
		}
		maxSize = Mathf.RoundToInt(maxSeconds * (1f/Time.fixedDeltaTime));
		storedInformation = new CircularStackVector3(maxSize);
		
		/*
		playerRigidbody = GetComponent<Rigidbody2D>();
		if (!playerRigidbody) {
			Debug.LogWarning("Player Rigidbody2D component not found.");
		}
		*/
		
		playerController = GetComponent<CharacterControl>();
		if (!playerController) {
			Debug.LogWarning("Player CharacterControl component not found.");
		}
		
		cooldown = new WaitForSeconds(rewindCoolDown);
		releaseButtonClear = new WaitUntil(RewindButtonReleased);

		audioSource = GetComponent<AudioSource>();
	}

	private void Update() {
		isRewindPressed = Input.GetButton("Rewind");

		DrawRewindParticles();
	}

	private void FixedUpdate() {
		//Debug.Log(storedInformation.GetSize());
		if (!isRewinding) {
			if (isRewindPressed && !isInCooldown) {
				EnterRewind();
			} else {
				StoreInformation(transform.position, playerRigidbody.velocity);
			}
		} else {
			if (!isRewindPressed) {
				StopRewind();
			} else {
				DoRewind();
			}
		}
	}

	void EnterRewind() {
		//Debug.Log("Enter Rewind");
		//playerRigidbody.isKinematic = true;
		isRewinding = true;
		playerController.FreezeMotion();

		if (audioSource) {
			int numPositions = storedInformation.GetSize();
			float clipLengthOriginal = audioSource.clip.length;
			float totalTime = Time.fixedDeltaTime * numPositions;

			audioSource.pitch = Mathf.Clamp(clipLengthOriginal / totalTime, -3f, 3f);

			audioSource.Play();

		}

		DoRewind();
	}

	void DoRewind() {
		//Debug.Log("Doing Rewind");
		if (!isRewindPressed || storedInformation.GetSize() <= 0) {
			StopRewind();
			return;
		}
		Information info = storedInformation.Pop();
		transform.position = info.position;
		playerRigidbody.velocity = info.velocity;
	}

	void StoreInformation(Vector3 position, Vector3 velocity) {
		//Debug.Log("Storing info");
		storedInformation.Push(new Information(position, velocity));
	}

	void StopRewind() {
		//Debug.Log("Stop Rewind");
		//playerRigidbody.isKinematic = false;
		isRewinding = false;
		playerController.UnFreezeMotion();
		//storedInformation.Clear();
		isInCooldown = true;

		if (audioSource) {
			audioSource.Stop();
		}

		StartCoroutine("ResetCooldown");
	}

	IEnumerator ResetCooldown() {
		yield return cooldown;
		yield return releaseButtonClear;
		isInCooldown = false;
	}

	bool RewindButtonReleased() {
		return !isRewindPressed;
	}

	void DrawRewindParticles() {
		Vector3[] positions = storedInformation.GetStackPositions();
		int size = positions.Length;

		for (int i = 0; i < size; i += 20) {
			ObjectPooler.Instance.SpawnFromPool(RewindParticleName, positions[i], particleRotation);
		}

	}

	public void ApplyOffsetTostoredInformation(Vector3 offset) {
		storedInformation.Offset(offset);
	}

	public float GetRewindMeter() {
		return (float)storedInformation.GetSize() / maxSize;
	}

	public bool GetIsInCooldown() {
		return isInCooldown;
	}

}
