using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]


public class Chunk : MonoBehaviour {
	
	private static int chunkXSize = World.chunkXSize;
	private static int chunkYSize = World.chunkYSize;
	Block[ , ] blocks = new Block[chunkXSize, chunkYSize];
	public bool update = false;
	public World world;
	public ChunkPos pos;
	private TriPos baseTriPos;

	MeshFilter filter;
	PolygonCollider2D coll;
	
	//Use this for initialization
	void Start () {
		filter = gameObject.GetComponent<MeshFilter>();
		coll = gameObject.GetComponent<PolygonCollider2D>();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log ("touch");
		if (coll.gameObject.tag == "breakDirt") {
			foreach (ContactPoint2D contact in coll.contacts) {
				WorldPos hitPos = new WorldPos(0, 0);
				for (float dX = -0.01f; dX < 0.02f; dX+= 0.01f) {
					for (float dY = -0.01f; dY < 0.02f; dY+= 0.01f) {
						hitPos.x = contact.point.x + dX;
						hitPos.y = contact.point.y + dY;
						TriPos triPos = hitPos.toTriPos();
						SetBlock(triPos, new BlockAir());
					}
				}
				Debug.DrawRay(new Vector3(hitPos.x, hitPos.y, 0), 
				              new Vector3(0.1f, 0.1f), Color.white, 10f, false);
				Debug.DrawRay(new Vector3(hitPos.x+0.1f, hitPos.y, 0), 
				              new Vector3(-0.1f, 0.1f), Color.white, 10f, false);
			}
			Object.Destroy (coll.gameObject);
		}
	}

	// Updates the chunk based on its contents
	void UpdateChunk()
	{
		System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
		st.Start();
		
		MeshData meshData = new MeshData();
		
		for (int x = 0; x < chunkXSize; x++)
		{
			for (int y = 0; y < chunkYSize; y++)
			{
				meshData = blocks[x, y].Blockdata(this, x, y, meshData);
			}
		}

		RenderMesh(meshData);
		UpdateCollider (meshData);

		st.Stop();
		Debug.Log(string.Format("Chunk took {0} ms to complete", st.ElapsedMilliseconds));
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

	public Block GetBlock(TriPos triPos)
	{
		if (baseTriPos == null)
			baseTriPos = pos.toTriPos (); //todo: cache better
		if(InRangeX(triPos.x) && InRangeY(triPos.y))
			return blocks[baseTriPos.x - triPos.x, baseTriPos.y - triPos.y];
		return world.GetBlock(triPos);
	}
	
	public bool InRangeX(int index)
	{
		if (baseTriPos==null)
			baseTriPos = pos.toTriPos (); //todo: cache better

		if (index < baseTriPos.x || index >= baseTriPos.x + chunkXSize)
			return false;
		
		return true;
	}

	public bool InRangeY(int index)
	{
		if (index < baseTriPos.y || index >= baseTriPos.y + chunkYSize)
			return false;
		
		return true;
	}
	
	public void SetBlock(TriPos triPos, Block block)
	{
		if (InRangeX(triPos.x) && InRangeY(triPos.y))
		{
			blocks[triPos.x - baseTriPos.x, triPos.y - baseTriPos.y] = block;
			update = true;
		}
		else
		{
			Debug.Log ("SetBlock on wrong chunk " + pos + " with tri " + triPos);
			//world.SetBlock(triPos, block);
		}
	}
}
