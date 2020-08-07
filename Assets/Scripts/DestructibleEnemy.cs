using UnityEngine;

public class DestructibleEnemy : MonoBehaviour, IDestroySelf {

    public void DestroySelf() {
		gameObject.SetActive(false);

		GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		if (gm) {
			gm.IncreaseRestoreHealthAmount(1);
		}

		// Play Death Animation
		ObjectPooler.Instance.SpawnFromPool("EnemyExplosion", transform);
	}

}
