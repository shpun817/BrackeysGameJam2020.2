using UnityEngine;

public class KillEnemy : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Enemy") {
			IDestroySelf destructibleEnemy = other.GetComponent<IDestroySelf>();
			if (destructibleEnemy != null) {
				destructibleEnemy.DestroySelf();
			}
		}
	}

}
