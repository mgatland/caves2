using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]


public class Chunk : MonoBehaviour {

	Block[ , ] blocks;
	public static int chunkSize = 16;
	public bool update = true;

	MeshFilter filter;
	PolygonCollider2D coll;


	//Use this for initialization
	void Start () {
		filter = gameObject.GetComponent<MeshFilter>();
		coll = gameObject.GetComponent<PolygonCollider2D>();
		
		//past here is just to set up an example chunk
		blocks = new Block[chunkSize, chunkSize];
		
		for (int x = 0; x < chunkSize; x++)
		{
			for (int y = 0; y < chunkSize; y++)
			{
				blocks[x, y] = new Block(x % 2 == 0);
			}
		}
		
		blocks[3, 3] = new BlockAir(3 % 2 == 0);
		
		UpdateChunk();		
	}

	// Updates the chunk based on its contents
	void UpdateChunk()
	{
		MeshData meshData = new MeshData();
		
		for (int x = 0; x < chunkSize; x++)
		{
			for (int y = 0; y < chunkSize; y++)
			{
				meshData = blocks[x, y].Blockdata(this, x, y, meshData);
			}
		}
		
		RenderMesh(meshData);
	}
	
	// Sends the calculated mesh information
	// to the mesh and collision components
	void RenderMesh(MeshData meshData)
	{
		filter.mesh.Clear();
		filter.mesh.vertices = meshData.vertices.ToArray();
		filter.mesh.triangles = meshData.triangles.ToArray();
		filter.mesh.uv = meshData.uv.ToArray();
		filter.mesh.RecalculateNormals();
	}

	//Update is called once per frame
	void Update () {
		
	}
	
	public Block GetBlock(int x, int y)
	{
		return blocks[x, y];
	}

	//Sends the calculated mesh information
	//to the mesh and collision components
	void RenderMesh()
	{
		
	}

	/*
	void Start() {
		Mesh mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;

		//Mesh mesh = GetComponent<MeshFilter>().mesh;
		//mesh.Clear();
		//x y z = left, up (not down), nothing

		int tris = 30;

		Vector3[] vertices = new Vector3[3 * tris];
		Vector2[] uv = new Vector2[3 * tris];
		int[] triangles = new int[3*tris];

		for (int i = 0; i < tris/2; i++) {

			int offset = i*6;

			vertices[0+offset] = new Vector3(0+i, 0, 0);
			vertices[1+offset] = new Vector3(0+i, 1, 0);
			vertices[2+offset] = new Vector3(1+i, 0, 0.3f);

			vertices[3+offset] = new Vector3(0+i, 1, 0);
			vertices[4+offset] = new Vector3(1+i, 1, 0);
			vertices[5+offset] = new Vector3(1+i, 0, 0.3f);

			uv[0+offset] = new Vector2(0, 0);
			uv[1+offset] = new Vector2(0, 1);
			uv[2+offset] = new Vector2(1, 1);

			uv[3+offset] = new Vector2(0, 0);
			uv[4+offset] = new Vector2(0, 1);
			uv[5+offset] = new Vector2(1, 1);

			for (int j = 0; j < 6; j++) {
				triangles[j+offset] = j + offset;
			}
		}
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;

		mesh.RecalculateNormals();

		PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
		Vector2[] points = {new Vector2(0,0), new Vector2(0, 1), new Vector2(1,0)};
		collider.SetPath(0, points);
	}*/

}
