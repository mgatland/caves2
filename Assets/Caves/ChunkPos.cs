using UnityEngine;
using System.Collections;

public class ChunkPos {
	public int x, y;
	
	public ChunkPos(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public WorldPos toWorldPos ()
	{
		int wX = this.x * World.chunkXSize * World.triSize / 2;
		int wY = this.y * World.chunkYSize * World.triSize;
		return new WorldPos (wX, wY);
	}

	//index of top left triangle in this chunk
	public TriPos toTriPos ()
	{
		int tX = this.x * World.chunkXSize;
		int tY = this.y * World.chunkYSize;
		return new TriPos (tX, tY);
	}

	public override bool Equals(object obj)
	{
		if (!(obj is ChunkPos))
			return false;
		
		ChunkPos pos = (ChunkPos)obj;
		return (pos.x == x && pos.y == y);
	}

	public override int GetHashCode ()
	{
		//via Jon Skeet
		int hash = 23;
		hash = hash * 31 + x;
		hash = hash * 31 + y;
		return hash;
	}

	public override string ToString() {
		return "ChunkPos(" + x + "," + y + ")";
	}
}
