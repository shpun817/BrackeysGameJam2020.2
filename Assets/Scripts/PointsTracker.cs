using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PointsTracker : MonoBehaviour, ISetup {

	public static PointsTracker Instance;

	[SerializeField]
	uint playerPoints;

	public Text pointsDisplay;

	IEnumerator increasePoints;
	[SerializeField]
	float increasePointsInterval = 0.5f;
	[SerializeField]
	uint increasePointsAmount = 888U;
	WaitForSecondsRealtime waitForIncreasePointsInterval;

	public 

	IEnumerator gameOver;
	WaitUntil waitUntilGameOver;
	bool isGameOver;

	private void Awake() {
		GameObject[] gameManagers = GameObject.FindGameObjectsWithTag("GameController");
		if (gameManagers.Length > 1) {

			foreach (GameObject obj in gameManagers) {
				if (obj == gameObject) { // Skip self
					continue;
				}
				PointsTracker tracker = obj.GetComponent<PointsTracker>();
				if (tracker) {
					tracker.Setup();
				}
			}

			Destroy(gameObject); // Destroy self
			return;
		} else {
			DontDestroyOnLoad(gameObject); // Keep self
		}

		Instance = this;
		waitUntilGameOver = new WaitUntil(IsGameOver);
		waitForIncreasePointsInterval = new WaitForSecondsRealtime(increasePointsInterval);
		Setup();
	}

	IEnumerator GameOver() {
		yield return waitUntilGameOver;

		if (increasePoints != null) {
			StopCoroutine(increasePoints);
		}
	}

	bool IsGameOver() {
		return isGameOver;
	}

	public void SetIsGameOver(bool gameOver) {
		isGameOver = gameOver;
	}

	public void Setup() {
		isGameOver = false;
		playerPoints = 0;

		increasePoints = IncreasePointsPeriodically();
		StartCoroutine(increasePoints);

		gameOver = GameOver();
		StartCoroutine(gameOver);
	}

	public uint GetPlayerPoints() {
		return playerPoints;
	}

	public void IncreasePoints(uint amount) {
		playerPoints += amount;
	}

	IEnumerator IncreasePointsPeriodically() {
		yield return waitForIncreasePointsInterval;
		playerPoints += increasePointsAmount;

		UpdatePointsDisplay();

		increasePoints = IncreasePointsPeriodically();
		StartCoroutine(increasePoints);
	}

	public string GetFormattedPoints() {

		uint points = playerPoints;
		if (points > 999999999U) {
			points = 999999999U;
		}
		string oldPointsAsString = points.ToString("D9");
		string newPointsAsString = "";

		for (int i = 0; i < oldPointsAsString.Length; ++i) {
			if (i % 3 == 0 && i != 0) {
				newPointsAsString += ",";
			}
			newPointsAsString += oldPointsAsString[i];
		}

		return newPointsAsString;

	}

	void UpdatePointsDisplay() {

		if (!pointsDisplay) {
			return;
		}

		pointsDisplay.text = GetFormattedPoints();
	}

}
