using UnityEngine;
using System.Collections;

public class Block {
	
	const float tileSize = 1f/128f*10; //for UVs

	//Base block constructor
	public Block(){
	}

	public virtual bool IsSolid()
	{
		return true;
	}

	public virtual MeshData Blockdata
		(Chunk chunk, int x, int y, MeshData meshData)
	{
		bool isUp = (x % 2 == 0);
		if (y % 2 == 0) isUp = !isUp;
		if (isUp) {
			meshData = FaceDataUp(chunk, x, y, meshData);
		} else {
			meshData = FaceDataDown(chunk, x, y, meshData);
		}

		return meshData;
	}

	private MeshData FaceDataUp
		(Chunk chunk, int x, int y, MeshData meshData)
	{
		meshData.AddVertex(new Vector3(x + 0, y * 2 + 0, 0.1f));
		meshData.AddVertex(new Vector3(x + 1, y * 2 + 2, 0.1f));
		meshData.AddVertex(new Vector3(x + 2, y * 2 + 0, 0.2f));
		meshData.AddTriangle();

		meshData.uv.Add(new Vector2(0, 0));
		meshData.uv.Add(new Vector2(0, 1*tileSize));
		meshData.uv.Add(new Vector2(1*tileSize, 1*tileSize));

		return meshData;
	}

	private MeshData FaceDataDown
		(Chunk chunk, int x, int y, MeshData meshData)
	{
		meshData.AddVertex(new Vector3(x, y * 2 + 2, 0.1f));
		meshData.AddVertex(new Vector3(x + 2, y * 2 + 2, 0.1f));
		meshData.AddVertex(new Vector3(x + 1, y * 2 + 0, 0.2f));
		meshData.AddTriangle();

		meshData.uv.Add(new Vector2(1*tileSize, 0));
		meshData.uv.Add(new Vector2(1*tileSize, 1*tileSize));
		meshData.uv.Add(new Vector2(2*tileSize, 1*tileSize));

		return meshData;
	}

}