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
		int cubeSide;
		float playerSpeed;
		float rotateSpeed;
		// float cubeWidth;

		public Player (GameObject playerCube)
		{
			cubeSide = 3;
			playerSpeed = 5f;
			rotateSpeed = 1f;

			container = new GameObject ("PlayerContainer");
			containerRB = container.AddComponent<Rigidbody2D> ();

			cubes = new GameObject[cubeSide, cubeSide];
			for (int y = 0; y < cubeSide; y++) {
				for (int x = 0; x < cubeSide; x++) {
					cubes [x, y] = GameObject.Instantiate (playerCube);
					cubes [x, y].transform.parent = container.transform;
					cubes [x, y].transform.localPosition = new Vector2 (-cubeSide/2 + x, -cubeSide/2 + y);
				}
			}
			// I do this after adding the cubes. If I do it before
			// then the cubes don't shrink.
			container.transform.localScale = Vector3.one / 2;
			// cubeWidth = cubes [0, 0].GetComponent<BoxCollider2D> ().size.x;
		}

		public void move ()
		{
			// Setting velocity
			containerRB.velocity = Vector2.zero;
			float hDir = Input.GetAxisRaw ("Horizontal");
			float vDir = Input.GetAxisRaw ("Vertical");
			Vector2 dir = new Vector2 (hDir, vDir).normalized;
			containerRB.velocity = dir * playerSpeed;

			// Rotating
			float rotateDir = Input.GetAxisRaw ("Aim");
			container.transform.Rotate(new Vector3(0, 0, -rotateDir * rotateSpeed));
		}

		public bool isAlive ()
		{
			int aliveCount = 0;
			foreach (GameObject cube in cubes) {
				if (cube.activeSelf) {
					aliveCount++;
				}
			}
			return aliveCount > 0;
		}
	}
}