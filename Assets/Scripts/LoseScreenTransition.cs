using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseScreenTransition : MonoBehaviour {

	public Button retryButton;
	public Button titleScreenButton;

    void Awake() {
        if (!retryButton) {
			Debug.LogError("Retry button not attached to " + gameObject.name);
		}
		
		if (!titleScreenButton) {
			Debug.LogError("Title Screen button not attached to " + gameObject.name);
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
