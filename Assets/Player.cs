using System;
using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public class Player
	{

		GameObject container;
		Rigidbody2D containerRB;
		GameObject[,] cubes;
		float playerSpeed;

		public Player (GameObject playerCube)
		{
			playerSpeed = 5f;

			container = new GameObject ("PlayerContainer");
			container.AddComponent<Rigidbody2D> ();
			containerRB = container.GetComponent<Rigidbody2D> ();

			cubes = new GameObject[3, 3];
			for (int y = 0; y < 3; y++) {
				for (int x = 0; x < 3; x++) {
					cubes [x, y] = GameObject.Instantiate (playerCube);
					cubes [x, y].transform.parent = container.transform;
					cubes [x, y].transform.localPosition = new Vector2 (x, y);
				}
			}
		}

		public void move ()
		{
			containerRB.velocity = Vector2.zero;
			float hDir = Input.GetAxisRaw ("Horizontal");
			float vDir = Input.GetAxisRaw ("Vertical");
			Vector2 dir = new Vector2 (hDir, vDir).normalized;
			containerRB.velocity = dir * playerSpeed;
		}
	}
}

