using UnityEngine;

public class DestructiblePickup : MonoBehaviour, IDestroySelf {

    public void DestroySelf() {
		gameObject.SetActive(false);

		// Play picked up animation
	}

}
