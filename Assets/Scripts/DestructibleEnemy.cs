using UnityEngine;

public class DestructibleEnemy : MonoBehaviour, IDestroySelf {

    public void DestroySelf() {
		gameObject.SetActive(false);

		// Play Death Animation
		ObjectPooler.Instance.SpawnFromPool("EnemyExplosion", transform);
	}

}
