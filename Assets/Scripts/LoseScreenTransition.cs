using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseScreenTransition : MonoBehaviour {

	public Button retryButton;
	public Button titleScreenButton;

    void Start() {
        if (!retryButton) {
			Debug.LogWarning("Retry button not attached to " + gameObject.name);
		}
		
		if (!titleScreenButton) {
			Debug.LogWarning("Title Screen button not attached to " + gameObject.name);
		}

		retryButton.onClick.AddListener(GoToLevel1);
		titleScreenButton.onClick.AddListener(GoToTitleScreen);
    }

    void GoToLevel1() {
		SceneManager.LoadScene("Level1");
	}

	void GoToTitleScreen() {
		SceneManager.LoadScene("TitleScreen");
	}

}
