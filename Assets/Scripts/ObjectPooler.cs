using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

	[System.Serializable]
	public class Pool {

		public string tag;
		public GameObject prefab;
		public int size;

	}

	#region Singleton

		public static ObjectPooler Instance;

		private void Awake() {
			Instance = this;
		}

	#endregion

	public Pool[] pools;

	public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start() {

		poolDictionary = new Dictionary<string, Queue<GameObject>>();

		GameObject pooledObjects = new GameObject("Pooled GameObjects");

		foreach (Pool pool in pools) {
			Queue<GameObject> objectPool = new Queue<GameObject>();

			GameObject objectType = new GameObject(pool.tag);
			objectType.transform.parent = pooledObjects.transform;

			for (int i = 0; i < pool.size; ++i) {
				GameObject obj = Instantiate(pool.prefab);

				/*
				ISetup objSetup = obj.GetComponent<ISetup>();
				if (objSetup != null) {
					objSetup.Setup();
				}
				*/

				obj.transform.parent = objectType.transform;
				obj.SetActive(false);
				objectPool.Enqueue(obj);
			}

			poolDictionary.Add(pool.tag, objectPool);
		}
        
    }

	public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation) {

		if (!poolDictionary.ContainsKey(tag)) {
			Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
			return null;
		}

		GameObject objectToSpawn =  poolDictionary[tag].Dequeue();

		objectToSpawn.SetActive(true);
		objectToSpawn.transform.position = position;
		objectToSpawn.transform.rotation = rotation;

		ISetup objectToSetUp = objectToSpawn.GetComponent<ISetup>();
		if (objectToSetUp != null) {
			objectToSetUp.Setup();
		}

		poolDictionary[tag].Enqueue(objectToSpawn);

		return objectToSpawn;

	}

	public void Despawn(GameObject obj) {
		IDestroySelf destructibleObject = obj.GetComponent<IDestroySelf>();
		if (destructibleObject != null) {
			destructibleObject.DestroySelf();
		}
	}

}
