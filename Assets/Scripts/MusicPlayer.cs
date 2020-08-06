using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private void Awake() {
		if (GameObject.FindGameObjectsWithTag("Music").Length > 1) {
			Destroy(gameObject);
		} else {
			DontDestroyOnLoad(gameObject);
		}
	}

}
