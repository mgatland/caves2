using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockWorld : MonoBehaviour {

	public static int triXSize = 2;
	public static int triYSize = 2;
	public GameObject chunkPrefab;

	// Use this for initialization
	void Start () {

		int tileSize = 16;
		MeshData meshData = new MeshData ();

		meshData.AddVertex(new Vector3(0, 0, 0.1f));
		meshData.AddVertex(new Vector3(1, 2, 0.1f));
		meshData.AddVertex(new Vector3(2, 0, 0.2f));
		meshData.AddTriangle();
		
		meshData.uv.Add(new Vector2(0, 0));
		meshData.uv.Add(new Vector2(0, 1*tileSize));
		meshData.uv.Add(new Vector2(1*tileSize, 1*tileSize));

		MeshFilter filter = gameObject.AddComponent<MeshFilter>();
		filter.mesh.Clear();
		filter.mesh.vertices = meshData.vertices.ToArray();
		filter.mesh.triangles = meshData.triangles.ToArray();
		filter.mesh.uv = meshData.uv.ToArray();
		filter.mesh.RecalculateNormals();


		for (int x = -20; x < 200; x++) {
			for (int y = -20; y < 200; y++) {
				CreateTri(x * triXSize / 2, y * triYSize);
			}
		}
	}

	void CreateTri(int x, int y) {

		GameObject tri = GameObject.CreatePrimitive(PrimitiveType.Cube);
		tri.AddComponent<Tri>();
		tri.transform.position = new Vector3(x, y, 0);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
