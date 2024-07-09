using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollisionsSetter : MonoBehaviour
{
	[SerializeField] ReturnSphere returnSphere;
	[SerializeField] CollisionSphere collisionSpherePrefab;
	[Range(0, 1f)]
	[SerializeField] private float maxRadius;
	public float MaxRadius;
	private int spawnCount;
	[SerializeField] private float[] speeds;
	[SerializeField] private Transform pivot;
	private int dir = -1;
	private float rot;
	private Vector3 rotVector3;
	public List<CollisionSphere> startSpheres;

	public static float ScreenWidth => Camera.main.orthographicSize * Camera.main.aspect;
	public static float ScreenHeight => Camera.main.orthographicSize;

	private void Start()
	{
		startSpheres = new List<CollisionSphere>();
		int currentLevel = DataAccessorSingleTone.Instance.Level;
		spawnCount = (int)(1.5f * Mathf.Log(currentLevel) + 5f);
		SpawnSpheres();

		rot = speeds[DataAccessorSingleTone.Instance.Horn];
	}

	private void Update()
	{
		rotVector3.z += dir * Time.deltaTime * rot;
		pivot.eulerAngles = rotVector3;
	}

	public void SpawnSpheres()
	{
		float currentAngle = 0;
		float stepAngle = 2 * Mathf.PI / spawnCount;
		Vector2 nextSpawn = new Vector2();
		MaxRadius = maxRadius * ScreenWidth - collisionSpherePrefab.Radius;

		for (int i = 0; i < spawnCount; i++)
		{
			nextSpawn.x = Mathf.Cos(currentAngle);
			nextSpawn.y = Mathf.Sin(currentAngle);
			nextSpawn *= MaxRadius;

			var sphere = Instantiate(collisionSpherePrefab, nextSpawn, Quaternion.identity, pivot);
			startSpheres.Add(sphere);
			sphere.collisionsSetter = this;

			currentAngle += stepAngle;
		}

		startSpheres[Random.Range(0, spawnCount)].SetActiveSphere();
	}

	public void ChooseRandomCollision(CollisionSphere currentSphere)
	{
		CollisionSphere sphere = null;
		var otherSpheres = startSpheres.Except(new List<CollisionSphere>() { currentSphere }).ToList();

		sphere = otherSpheres[Random.Range(0, spawnCount - 1)];
		sphere.SetActiveSphere();
	}
}
