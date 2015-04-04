using UnityEngine;
using System.Collections;

public class TriPos {
	public int x, y;
	
	public TriPos(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public ChunkPos toChunkPos ()
	{
		int cX = this.x / World.chunkXSize;
		int cY = this.y / World.chunkYSize;
		return new ChunkPos (cX, cY);
	}
	
	public override bool Equals(object obj)
	{
		if (!(obj is TriPos))
			return false;
		
		TriPos pos = (TriPos)obj;
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
		return "TriPos(" + x + "," + y + ")";
	}
}
