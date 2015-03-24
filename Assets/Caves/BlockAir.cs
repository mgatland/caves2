using UnityEngine;
using System.Collections;

public class BlockAir : Block
{
	public BlockAir()
		: base()
	{
		
	}
	
	public override MeshData Blockdata
		(Chunk chunk, int x, int y, MeshData meshData)
	{
		return meshData;
	}
	
	public override bool IsSolid()
	{
		return false;
	}
}