using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	WaitUntil waitUntilEndOfSong;

	public AudioSource audioSource;

    private void Awake() {
		if (GameObject.FindGameObjectsWithTag("Music").Length > 1) {
			Destroy(gameObject);
		} else {
			DontDestroyOnLoad(gameObject);
		}
		waitUntilEndOfSong = new WaitUntil(endOfSong);
	}

	private void Start() {
		StartCoroutine(LoopBackSong());
	}

	bool endOfSong() {
		return (!audioSource.isPlaying);
	}

	IEnumerator LoopBackSong() {
		yield return waitUntilEndOfSong;
		audioSource.time = 13f;
		audioSource.Play();
		StartCoroutine(LoopBackSong());
	}

}
