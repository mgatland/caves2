using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

	public Dictionary<WorldPos, Chunk> chunks = new Dictionary<WorldPos, Chunk>();
	public GameObject chunkPrefab;

	public static int chunkXSize = 16;
	public static int chunkYSize = 8;
	public static int triSize = 2;

	// Use this for initialization
	void Start () {

		for (int x = -2; x < 2; x++) {
			for (int y = -2; y < 2; y++) {
				CreateChunk(x * chunkXSize * triSize / 2, y * chunkYSize * triSize);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreateChunk(int x, int y)
	{
		WorldPos worldPos = new WorldPos(x, y);
		
		//Instantiate the chunk at the coordinates using the chunk prefab
		GameObject newChunkObject = Instantiate(
			chunkPrefab, new Vector3(x, y),
			Quaternion.Euler(Vector3.zero)
			) as GameObject;
		
		Chunk newChunk = newChunkObject.GetComponent<Chunk>();
		
		newChunk.pos = worldPos;
		newChunk.world = this;
		
		//Add it to the chunks dictionary with the position as the key
		chunks.Add(worldPos, newChunk);

		for (int xi = 0; xi < chunkXSize; xi++)
		{
			for (int yi = 0; yi < chunkYSize; yi++)
			{
				SetBlock(x + xi, y + yi, new Block());
			}
		}
	}

	public Chunk GetChunk(int x, int y)
	{
		WorldPos pos = new WorldPos();
		float multipleX = chunkXSize * triSize / 2;
		float multipleY = chunkYSize * triSize;
		pos.x = Mathf.FloorToInt(x / multipleX ) * chunkXSize * triSize / 2;
		pos.y = Mathf.FloorToInt(y / multipleY ) * chunkYSize * triSize;
		
		Chunk containerChunk = null;
		chunks.TryGetValue(pos, out containerChunk);
		return containerChunk;
	}
	
	public Block GetBlock(int x, int y)
	{
		Chunk containerChunk = GetChunk(x, y);
		if (containerChunk != null)
		{
			Block block = containerChunk.GetBlock(
				x - containerChunk.pos.x,
				y - containerChunk.pos.y);
			return block;
		}
		else
		{
			return new BlockAir();
		}
	}

	public void SetBlock(int x, int y, Block block)
	{
		Chunk chunk = GetChunk(x, y);
		
		if (chunk != null)
		{
			chunk.SetBlock(x - chunk.pos.x, y - chunk.pos.y, block);
			chunk.update = true;
		}
	}

	public void DestroyChunk(int x, int y)
	{
		Chunk chunk = null;
		if (chunks.TryGetValue(new WorldPos(x, y), out chunk))
		{
			Object.Destroy(chunk.gameObject);
			chunks.Remove(new WorldPos(x, y));
		}
	}
}
