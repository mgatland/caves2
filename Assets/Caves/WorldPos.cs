using UnityEngine;
using System.Collections;

public struct WorldPos
{
	public int x, y;
	
	public WorldPos(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is WorldPos))
			return false;
		
		WorldPos pos = (WorldPos)obj;
		return (pos.x == x && pos.y == y);
	}
}