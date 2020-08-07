using UnityEngine;

public class DestructiblePickup : MonoBehaviour, IDestroySelf {

    public void DestroySelf() {
		gameObject.SetActive(false);

		PointsTracker.Instance.IncreasePoints(333U);
	}

}
