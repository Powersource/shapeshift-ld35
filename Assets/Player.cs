using System;
using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public class Player
	{

		GameObject container;
		GameObject[,] cubes;

		public Player (GameObject playerCube)
		{
			container = new GameObject ("PlayerContainer");
			cubes = new GameObject[3,3];
			for (int y = 0; y < 3; y++) {
				for (int x = 0; x < 3; x++) {
					cubes [x, y] = GameObject.Instantiate (playerCube);
					cubes [x, y].transform.parent = container.transform;
					cubes [x, y].transform.localPosition = new Vector2 (x, y);
				}
			}
		}
	}
}

