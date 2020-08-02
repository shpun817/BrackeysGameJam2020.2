using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindTime : MonoBehaviour {

	public float maxSeconds = 3f;
	public float rewindCoolDown = 0.35f;

	CircularStack<Vector3> storedPositions;
	Transform playerTransform;
	//Rigidbody2D playerRigidbody;
	CharacterControl playerController;
	bool isRewindPressed = false;
	bool isRewinding = false;
	bool isInCooldown = false;
	int maxSize;

	WaitForSeconds cooldown;
	WaitUntil releaseButtonClear;

    private void Awake() {
		if (maxSeconds <= 0) {
			Debug.LogWarning("Invalid input to Max Seconds in RewindTime.");
		}
		maxSize = Mathf.RoundToInt(maxSeconds * (1f/Time.fixedDeltaTime));
		storedPositions = new CircularStack<Vector3>(maxSize);
		playerTransform = GetComponent<Transform>();
		
		/*
		playerRigidbody = GetComponent<Rigidbody2D>();
		if (!playerRigidbody) {
			Debug.LogError("Player Rigidbody2D component not found.");
		}
		*/
		
		playerController = GetComponent<CharacterControl>();
		if (!playerController) {
			Debug.LogError("Player CharacterControl component not found.");
		}
		
		cooldown = new WaitForSeconds(rewindCoolDown);
		releaseButtonClear = new WaitUntil(RewindButtonReleased);
	}

	private void Update() {
		isRewindPressed = Input.GetButton("Rewind");
	}

	private void FixedUpdate() {
		//Debug.Log(storedPositions.GetSize());
		if (!isRewinding) {
			if (isRewindPressed && !isInCooldown) {
				EnterRewind();
			} else {
				StoreInformation(playerTransform.position);
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
		DoRewind();
	}

	void DoRewind() {
		//Debug.Log("Doing Rewind");
		if (!isRewindPressed || storedPositions.GetSize() <= 0) {
			StopRewind();
			return;
		}
		playerTransform.position = storedPositions.Pop();
	}

	void StoreInformation(Vector3 position) {
		//Debug.Log("Storing info");
		storedPositions.Push(position);
	}

	void StopRewind() {
		//Debug.Log("Stop Rewind");
		//playerRigidbody.isKinematic = false;
		isRewinding = false;
		playerController.UnFreezeMotion();
		//storedPositions.Clear();
		isInCooldown = true;
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

	public float GetRewindMeter() {
		return (float)storedPositions.GetSize() / maxSize;
	}

	public bool GetIsInCooldown() {
		return isInCooldown;
	}

}
