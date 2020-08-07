using UnityEngine;
using UnityEngine.UI;

public class MenuPointsDisplay : MonoBehaviour {

    private void Start() {
		Text text = GetComponent<Text>();
		if (text) {
			text.text = PointsTracker.Instance.GetFormattedPoints();
		}
	}

}
