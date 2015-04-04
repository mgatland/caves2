using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

	//from http://alexstv.com/index.php/posts/unity-voxel-block-tutorial-pt-4

	//x and y == World coordinates == Unity coordinates == WorldPos
	//chunkX and chunkY == chunk index == ChunkPos
	//tX and tY == triangle index. Global, not relative to a particular chunk
	//ctX and ctY refer to triangle index within one chunk.


	public Dictionary<ChunkPos, Chunk> chunks = new Dictionary<ChunkPos, Chunk>();
	public GameObject chunkPrefab;

	public static int chunkXSize = 16;
	public static int chunkYSize = 8;
	public static int triSize = 2;

	// Use this for initialization
	void Start () {
		int minX = 0;
		int minY = 0;
		int maxX = 4;
		int maxY = 4;
		for (int chunkX = minX; chunkX < maxX; chunkX++) {
			for (int chunkY = minY; chunkY < maxY; chunkY++) {
				ChunkPos pos = new ChunkPos(chunkX, chunkY);
				CreateChunk(pos);
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	

		/*Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		TriPos triPos = new WorldPos(mousePos.x, mousePos.y).toTriPos();
		if (Random.Range (0f, 1f) > 0.5) {
			SetBlock (triPos, new BlockAir ());
		} else {
			SetBlock (triPos, new Block ());
		}*/
	}

	public void CreateChunk(ChunkPos chunkPos)
	{
		WorldPos worldPos = chunkPos.toWorldPos();

		//Instantiate the chunk at its world coordinates
		GameObject newChunkObject = Instantiate(
			chunkPrefab, new Vector3(worldPos.x, worldPos.y),
			Quaternion.Euler(Vector3.zero)
			) as GameObject;
		
		Chunk newChunk = newChunkObject.GetComponent<Chunk>();
		newChunk.pos = chunkPos;
		newChunk.world = this;
		
		//Add it to the chunks dictionary with the position as the key
		chunks.Add(chunkPos, newChunk);

		TriPos startTriPos = chunkPos.toTriPos ();

		for (int xi = 0; xi < chunkXSize; xi++)
		{
			for (int yi = 0; yi < chunkYSize; yi++)
			{
				TriPos triPos = new TriPos(startTriPos.x + xi, startTriPos.y + yi);
				SetBlock(triPos, new Block());
			}
		}
	}

	public Chunk GetChunk(TriPos triPos)
	{
		ChunkPos pos = triPos.toChunkPos ();
		Chunk containerChunk = null;
		chunks.TryGetValue(pos, out containerChunk);
		if (containerChunk == null) {
			Debug.Log ("GetChunk: No chunk at " + pos.ToString ());
		}
		return containerChunk;
	}
	
	public Block GetBlock(TriPos triPos)
	{
		Chunk containerChunk = GetChunk(triPos);
		if (containerChunk != null)
		{
			Block block = containerChunk.GetBlock(triPos);
			return block;
		}
		else
		{
			return new BlockAir();
		}
	}

	public void SetBlock(TriPos triPos, Block block)
	{
		Chunk chunk = GetChunk(triPos);
		
		if (chunk != null) {
			chunk.SetBlock (triPos, block);
			chunk.update = true;
		} else {
			Debug.Log ("SetBlock: No chunk at " + triPos.toChunkPos().ToString());
		}
	}

	public void DestroyChunk(int notimplemented)
	{
		//Chunk chunk = null;
		//if (chunks.TryGetValue(new WorldPos(x, y), out chunk))
		//{
		//	Object.Destroy(chunk.gameObject);
		//	chunks.Remove(new WorldPos(x, y));
		//}
	}
}
