using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

	#region Singleton

		public static ObjectSpawner Instance;

		private void Awake() {
			Instance = this;
		}

	#endregion

	[System.Serializable]
	class ObjectToSpawn {
		[SerializeField]
		string tag;
		[SerializeField]
		float weight;
	}

	[SerializeField]
	ObjectToSpawn[] objectsToSpawn;

	[SerializeField]
	float spawnDistanceFromPlayer = 7f; // How far down at least an object is spawned

	[SerializeField]
	float spawnAreaHeight = 10f;

	[SerializeField]
	float spawnAreaWidth = 20f;

	[SerializeField]
	float spawnDistanceFromEachOther = 1.5f;

	[SerializeField]
	float spawnTimeInterval = 2.5f;

	WaitForSeconds waitForSpawnInterval;
	Quaternion spawnRotation;

    // Start is called before the first frame update
    void Start() {
        waitForSpawnInterval = new WaitForSeconds(spawnTimeInterval);
		spawnRotation = transform.rotation;
		StartCoroutine(SpawnObjects());
    }

	IEnumerator SpawnObjects() {
		yield return waitForSpawnInterval;

		List<Vector3> spawnPositions = CalculateSpawnPositions();

		string[] spawnObjectTags = GenerateSpawnObjectTags(spawnPositions.Count);

		SpawnObjectsWithTagsAtPositions(spawnObjectTags, spawnPositions);
		

		StartCoroutine(SpawnObjects());
	}

	List<Vector3> CalculateSpawnPositions() {


		return null;
	}

	string[] GenerateSpawnObjectTags(int numObjects) {

		return null;
	}

	void SpawnObjectsWithTagsAtPositions(string[] tags, List<Vector3> positions) {
		if (positions.Count != tags.Length) {
			Debug.LogWarning("Numbers of elements in spawn positions and spawn object tags do not match.");
			return;
		}
		int size = tags.Length;
		for (int i = 0; i < size; ++i) {
			ObjectPooler.Instance.SpawnFromPool(tags[i], positions[i], spawnRotation);
		}
	}

}
