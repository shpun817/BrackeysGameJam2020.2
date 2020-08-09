using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCamera : MonoBehaviour {

	public Transform background;

	public float spawnCloudDistance = 8f;

	public string[] cloudNames;

	new Camera camera;
	float halfHeight, halfWidth; // of the camera

	Quaternion spawnRotation;

	WaitUntil waitUntilCameraReachesLowestY;

	float lowestY = 0f;

	public void ResetLowY() {
		lowestY = 0f;
	}

	private void Start() {
		camera = transform.parent.gameObject.GetComponent<Camera>();
		halfHeight = 16f;
		halfWidth = 32f;
		spawnRotation = transform.rotation;
		waitUntilCameraReachesLowestY = new WaitUntil(CameraReachesLowestY);

		StartCoroutine(SpawnClouds());
	}

	IEnumerator SpawnClouds() {
		yield return waitUntilCameraReachesLowestY;

		List<Vector3> positionsToSpawn = CalculatePositionsToSpawn();

		SpawnCloudsAtPositions(positionsToSpawn);

		StartCoroutine(SpawnClouds());
	}

	List<Vector3> CalculatePositionsToSpawn() {

		List<Vector3> positions = new List<Vector3>();

		float lowX = transform.position.x - halfWidth;
		float highX = transform.position.x + halfWidth;
		float lowY = transform.position.y - 32f - halfHeight;
		float highY = transform.position.y - 32f + halfHeight;

		lowestY = lowY + halfHeight;

		float jiggleAmount = spawnCloudDistance * 0.4f;

		for (float i = lowY; i <= highY; i += spawnCloudDistance) {
			for (float j = lowX; j <= highX; j += spawnCloudDistance) {

				float jiggleX = Random.Range(-jiggleAmount, jiggleAmount);
				float jiggleY = Random.Range(-jiggleAmount, jiggleAmount);

				positions.Add(new Vector3(j + jiggleX, i + jiggleY, 0f));

			}
		}

		return positions;
	}

	void SpawnCloudsAtPositions(List<Vector3> positions) {

		int size = positions.Count;

		for (int i = 0; i < size; ++i) {

			string name = cloudNames[Random.Range(0, cloudNames.Length)];

			if (name == "None")
				continue;
			
			GameObject obj = ObjectPooler.Instance.SpawnFromPool(name, positions[i], spawnRotation);
		
			obj.transform.parent = background;

		}
	}

	bool CameraReachesLowestY() {
		return transform.position.y <= lowestY;
	}

}
