using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

	/* singleton
	#region Singleton

		public static ObjectSpawner Instance;

		private void Awake() {
			Instance = this;
		}

	#endregion
	*/

	[System.Serializable]
	public class ObjectToSpawn {
		public string tag;
		[Range(0f, 1f)]
		public float weight;
	}

	public ObjectToSpawn[] objectsToSpawn;

	[SerializeField]
	float spawnDistanceFromPlayer = 7f; // How far down at least an object is spawned

	[SerializeField]
	float spawnAreaHeight = 10f;

	[SerializeField]
	float spawnAreaWidth = 20f;

	[SerializeField]
	float spawnDistanceFromEachOther = 1.5f;

	float totalWeight;

	WaitUntil waitUntilPlayerReachesLowY;
	float lowY = -5f;
	Quaternion spawnRotation;

	Transform playerTransform;

    // Start is called before the first frame update
    void Start() {
        waitUntilPlayerReachesLowY = new WaitUntil(playerReachesLowY);
		spawnRotation = transform.rotation;
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		if (!playerTransform) {
			Debug.LogWarning("Player transform not found by Object Spawner.");
		}
		totalWeight = 0f;
		foreach (ObjectToSpawn obj in objectsToSpawn) {
			totalWeight += obj.weight;
		}
		StartCoroutine(SpawnObjects());
    }

	IEnumerator SpawnObjects() {
		yield return waitUntilPlayerReachesLowY;

		List<Vector3> spawnPositions = CalculateSpawnPositions();

		string[] spawnObjectTags = GenerateSpawnObjectTags(spawnPositions.Count);

		SpawnObjectsWithTagsAtPositions(spawnObjectTags, spawnPositions);

		StartCoroutine(SpawnObjects());
	}

	List<Vector3> CalculateSpawnPositions() {

		List<Vector3> positions = new List<Vector3>();

		float startY = playerTransform.position.y - spawnDistanceFromPlayer;
		float endY = playerTransform.position.y - spawnDistanceFromPlayer - spawnAreaHeight;

		lowY = endY;

		float startX = playerTransform.position.x - (spawnAreaWidth * 0.5f);
		float endX = playerTransform.position.x + (spawnAreaWidth * 0.5f);

		float jiggleAmount = spawnDistanceFromEachOther * 0.3f;

		for (float i = startY; i >= endY; i -= spawnDistanceFromEachOther) {
			for (float j = startX; j <= endX; j += spawnDistanceFromEachOther) {

				float jiggleX = Random.Range(-jiggleAmount, jiggleAmount);
				float jiggleY = Random.Range(-jiggleAmount, jiggleAmount);

				positions.Add(new Vector3(j + jiggleX, i + jiggleY, 0f));

			}
		}

		return positions;
	}

	string[] GenerateSpawnObjectTags(int numObjects) {

		string[] tags = new string[numObjects];

		for (int i = 0; i < numObjects; ++i) {

			float determinant = Random.Range(0f, totalWeight);

			int objectIndex = 0;

			int numTypes = objectsToSpawn.Length;

			foreach (ObjectToSpawn obj in objectsToSpawn) {

				if (determinant <= obj.weight) {
					break;
				}

				++objectIndex;

				determinant -= obj.weight;

			}

			// To play safe
			if (objectIndex >= numTypes) {
				objectIndex = numTypes - 1;
			}

			tags[i] = objectsToSpawn[objectIndex].tag;

		}

		return tags;
	}

	void SpawnObjectsWithTagsAtPositions(string[] tags, List<Vector3> positions) {
		if (positions.Count != tags.Length) {
			Debug.LogWarning("Numbers of elements in spawn positions and spawn object tags do not match.");
			return;
		}
		int size = tags.Length;
		for (int i = 0; i < size; ++i) {
			if (tags[i] != "None")
				ObjectPooler.Instance.SpawnFromPool(tags[i], positions[i], spawnRotation);
		}
	}

	bool playerReachesLowY() {
		if (!playerTransform) {
			playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

			if (!playerTransform)
				return false;
		}

		if (playerTransform.position.y <= lowY) {
			return true;
		} else {
			return false;
		}
	}

}
