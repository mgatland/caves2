using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]


public class Chunk : MonoBehaviour {
	
	private static int chunkSize = World.chunkSize;
	Block[ , ] blocks = new Block[chunkSize, chunkSize];
	public bool update = false;
	public World world;
	public WorldPos pos;

	MeshFilter filter;
	PolygonCollider2D coll;
	
	//Use this for initialization
	void Start () {
		filter = gameObject.GetComponent<MeshFilter>();
		coll = gameObject.GetComponent<PolygonCollider2D>();
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
		UpdateCollider (meshData);
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

	void UpdateCollider (MeshData meshData) 
	{
		int i = 0;
		coll.pathCount = meshData.colPaths.Count;
		foreach (List<Vector2> path in meshData.colPaths) {
			coll.SetPath (i, path.ToArray());
			i++;
		}
	}

	void Update () {
		if (update)
		{
			update = false;
			UpdateChunk();
		}
	}

	public Block GetBlock(int x, int y)
	{
		if(InRange(x) && InRange(y))
			return blocks[x, y];
		return world.GetBlock(pos.x + x, pos.y + y);
	}
	
	public static bool InRange(int index)
	{
		if (index < 0 || index >= chunkSize)
			return false;
		
		return true;
	}

	public void SetBlock(int x, int y, Block block)
	{
		if (InRange(x) && InRange(y))
		{
			blocks[x, y] = block;
		}
		else
		{
			world.SetBlock(pos.x + x, pos.y + y, block);
		}
	}
}
