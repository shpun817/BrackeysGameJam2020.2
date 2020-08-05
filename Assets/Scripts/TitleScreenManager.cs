using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour {

    public Button aboutButton, startButton, creditButton;

	public GameObject aboutPanel, creditPanel;

	private void Start() {
		if (!aboutButton) {
			Debug.LogError("About button not attached to " + gameObject.name);
		}
		
		if (!startButton) {
			Debug.LogError("Start button not attached to " + gameObject.name);
		}
		
		if (!creditButton) {
			Debug.LogError("Credit button not attached to " + gameObject.name);
		}

		if (!aboutPanel) {
			Debug.LogError("About panel not attached to " + gameObject.name);
		}

		if (!creditPanel) {
			Debug.LogError("Credit panel not attached to " + gameObject.name);
		}

		aboutButton.onClick.AddListener(() => ShowOrHidePanel(aboutPanel));
		startButton.onClick.AddListener(GoToLevel1);
		creditButton.onClick.AddListener(() => ShowOrHidePanel(creditPanel));
	}

	void GoToLevel1() {
		SceneManager.LoadScene("Level1");
	}

	void ShowOrHidePanel(GameObject panel) {
		if (panel.activeInHierarchy) {
			panel.SetActive(false);
		} else {
			panel.SetActive(true);
		}
	}

}
