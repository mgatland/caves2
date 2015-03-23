using UnityEngine;
using System.Collections;

public class Block {

	private bool IsUp;
	const float tileSize = 1f/128f*10;

	//Base block constructor
	public Block(bool IsUp){
		this.IsUp = IsUp;
	}

	public virtual bool IsSolid()
	{
		return true;
	}

	
	public virtual MeshData Blockdata
		(Chunk chunk, int x, int y, MeshData meshData)
	{
		if (IsUp) {
			meshData = FaceDataUp(chunk, x, y, meshData);
		} else {
			meshData = FaceDataDown(chunk, x, y, meshData);
		}

		return meshData;
	}

	private MeshData FaceDataUp
		(Chunk chunk, int x, int y, MeshData meshData)
	{
		meshData.vertices.Add(new Vector3(x/2 + 0, y + 0, 0.1f));
		meshData.vertices.Add(new Vector3(x/2 + 0.5f, y + 1, 0.2f));
		meshData.vertices.Add(new Vector3(x/2 + 1, y + 0, 0.3f));
		meshData.AddTriangle();

		meshData.uv.Add(new Vector2(0, 0));
		meshData.uv.Add(new Vector2(0, 1*tileSize));
		meshData.uv.Add(new Vector2(1*tileSize, 1*tileSize));

		return meshData;
	}

	private MeshData FaceDataDown
		(Chunk chunk, int x, int y, MeshData meshData)
	{
		meshData.vertices.Add(new Vector3(x/2 + 0.5f, y + 1, 0.4f));
		meshData.vertices.Add(new Vector3(x/2 + 1.5f, y + 1, 0.5f));
		meshData.vertices.Add(new Vector3(x/2 + 1, y + 0, 0.6f));
		meshData.AddTriangle();

		meshData.uv.Add(new Vector2(1*tileSize, 0));
		meshData.uv.Add(new Vector2(1*tileSize, 1*tileSize));
		meshData.uv.Add(new Vector2(2*tileSize, 1*tileSize));

		return meshData;
	}

}