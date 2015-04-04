using UnityEngine;
using System.Collections;

public class WorldPos {
	public float x, y;
	
	public WorldPos(float x, float y)
	{
		this.x = x;
		this.y = y;
	}

	//Which triangle contains this point?
	public TriPos toTriPos ()
	{
		int tY = (int)(this.y / World.triSize);
		int maxTX = (int)this.x * 2 / World.triSize;
		int minTX = maxTX - 1;
		int tX = 0;

		//For the line math, y = 1 is the top of our row, y = 0 is the bottom
		float yInRow = (this.y - tY * World.triSize) / World.triSize;
		//Are we in a section with an upwards slope or downwards slope ?
		if ((minTX % 2 != 0) == (tY % 2 == 0)) {
			//downwards slope \
			if (yInRow > -1f * this.x + minTX + 2) {
				tX = maxTX;
			} else {
				tX = minTX;
			}
		} else {
			//upwards slope /
			if (yInRow > 1f * this.x - minTX - 1) {
				tX = minTX;
			} else {
				tX = maxTX;
			}
		}
		return new TriPos (tX, tY);
	}

	public override bool Equals(object obj)
	{
		if (!(obj is WorldPos))
			return false;
		
		WorldPos pos = (WorldPos)obj;
		return (pos.x == x && pos.y == y);
	}

	public override int GetHashCode ()
	{
		//via Jon Skeet
		int hash = 23;
		hash = hash * 31 + x.GetHashCode();
		hash = hash * 31 + y.GetHashCode();
		return hash;
	}
}
