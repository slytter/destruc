﻿using UnityEngine;
using System.Collections;

public class meshmodify : MonoBehaviour
{
	Mesh mesh;
	Vector3[] vertixes;
	Vector3[] startVertices;

	[Range (0, 0.5f)]
	public float density = 2f;
	[Range (0, 100)]
	public float height = 2f;

	public bool liveUpdate = false;
	public bool followPlayer = false;
	public GameObject player;

	public int lenght = 51;
	float ObjectPosX;
	float ObjectPosY;
	public int sizeDensity = 2;

	GameObject destructionState;
	//destruction destruction;
	// Use this for initialization
	public void Awake ()
	{
		destructionState = GameObject.Find ("destructionState");
		//destruction = destructionState.GetComponent<destruction> ();

		mesh = GetComponent<MeshFilter> ().mesh;
		vertixes = mesh.vertices;
//		print(vertixes.GetLength (0));
		startVertices = (Vector3[])mesh.vertices.Clone ();
		ObjectPosX = gameObject.transform.position.x;
		ObjectPosY = gameObject.transform.position.z;
		generatePerlin ();
	}



	void Update ()
	{
		if (liveUpdate) {
			generatePerlin ();
		}
		if (followPlayer) {
			gameObject.transform.position = new Vector3 (player.transform.position.x, gameObject.transform.position.y, player.transform.position.z);
		}
	}

	// Update is called once per frame
	public void generatePerlin ()
	{
		ObjectPosX = gameObject.transform.position.x;
		ObjectPosY = gameObject.transform.position.z;
		float seed = 0;
		for (float y = 0; y < lenght; y++) {
			for (float x = 0; x < lenght; x++) {

				int i = (int)(y * lenght + x);
				float xP = (x - ObjectPosX / sizeDensity) * density;
				float yP = (y - ObjectPosY / sizeDensity) * density;

				float widePerlin = Mathf.PerlinNoise (xP / 8, yP / 8);
				float modifiedHeight = height * widePerlin / 2;
				float perlin = Mathf.PerlinNoise (xP, yP) * modifiedHeight;
				float perlin2 = Mathf.PerlinNoise (xP * 2, yP * 2) * modifiedHeight / 4;
				float perlin3 = Mathf.PerlinNoise (xP * 4, yP * 4) * modifiedHeight / 8;
				float total = seed + Mathf.Sin (perlin) + perlin / 1.5f + perlin2 + perlin3 + widePerlin;
				vertixes [i] = startVertices [i] + new Vector3 (0f, total, 0f);

				//int xd = (int)(-(ObjectPosX + (x * sizeDensity)) + 50f);
				//	int yd = (int)(-(ObjectPosY + (y * sizeDensity)) + 50f);
				//vertixes [i].y += destruction.getDestruction ((int)(xP * 50 - 50), (int)(yP * 50 - 50));
				//Debug.DrawRay (new Vector3 (xP * 50 - 50, 0, yP * 50 - 50), new Vector3 (0, 40, 0), Color.blue, 1f);

			

			}
		}
		
		mesh.vertices = vertixes;
		mesh.RecalculateNormals ();
		MeshCollider mc = GetComponent<MeshCollider> ();
		mc.sharedMesh = null;
		mc.sharedMesh = mesh;
	}
}
