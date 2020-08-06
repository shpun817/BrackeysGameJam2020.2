using UnityEngine;

public class PickupSoundEffect : MonoBehaviour, ISetup {

	public AudioSource audioSource;

    public void Setup() {
		if (audioSource) {
			audioSource.time = 0f;
			if (!audioSource.isPlaying) {
				audioSource.Play();
			}
		}
	}

}
