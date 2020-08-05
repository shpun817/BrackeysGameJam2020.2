using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreenTransition : MonoBehaviour {

	public Button titleScreenButton;

    void Start() {
		
		if (!titleScreenButton) {
			Debug.LogWarning("Title Screen button not attached to " + gameObject.name);
		}

		titleScreenButton.onClick.AddListener(GoToTitleScreen);
    }

	void GoToTitleScreen() {
		SceneManager.LoadScene("TitleScreen");
	}

}
