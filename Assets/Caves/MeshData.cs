//MeshData.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshData {
	public List<Vector3> vertices = new List<Vector3>();
	public List<int> triangles = new List<int>();
	public List<Vector2> uv = new List<Vector2>();
	
	private List<Vector2> colPath = new List<Vector2>();
	public List<List<Vector2>> colPaths = new List<List<Vector2>>();
	
	public MeshData() { }
	
	public void AddTriangle()
	{
		triangles.Add(vertices.Count - 3);
		triangles.Add(vertices.Count - 2);
		triangles.Add(vertices.Count - 1);
		colPaths.Add (colPath);
		colPath = new List<Vector2> ();
	}

	public void AddVertex (Vector3 vector3)
	{
		vertices.Add (vector3);
		colPath.Add (new Vector2(vector3.x, vector3.y));
	}
}