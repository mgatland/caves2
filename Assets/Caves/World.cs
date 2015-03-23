using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

	public Dictionary<WorldPos, Chunk> chunks = new Dictionary<WorldPos, Chunk>();
	public GameObject chunkPrefab;

	public static int chunkSize = 16;

	// Use this for initialization
	void Start () {
		for (int x = -1; x < 1; x++) {
			for (int y = -1; y < 1; y++) {
				CreateChunk(x * chunkSize, y * chunkSize);
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

		for (int xi = 0; xi < chunkSize; xi++)
		{
			for (int yi = 0; yi < chunkSize; yi++)
			{
				SetBlock(x + xi, y + yi, new Block(xi % 2 == 0));
			}
		}
	}

	public Chunk GetChunk(int x, int y)
	{
		WorldPos pos = new WorldPos();
		float multiple = chunkSize;
		pos.x = Mathf.FloorToInt(x / multiple ) * chunkSize;
		pos.y = Mathf.FloorToInt(y / multiple ) * chunkSize;
		
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
				y -containerChunk.pos.y);
			return block;
		}
		else
		{
			return new BlockAir(x % 2 == 0);
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
