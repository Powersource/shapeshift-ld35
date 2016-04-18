using System;
using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public class Player
	{

		GameObject container;
		Rigidbody2D containerRB;
		GameObject playerCube;
		GameObject[,] cubes;
		AudioClip soundClip;
		AudioSource sound;
		int cubeSide;
		float playerSpeed;
		float rotateSpeed;
		float cubeWidth;
		float playerFireDelay;
		float playerFireCountdown;
		float playerBulletSpeed;
		float xLimit;
		float yLimit;

		public Player ()
		{
			cubeSide = 3;
			playerSpeed = 5f;
			rotateSpeed = 60f;
			// In seconds
			playerFireDelay = 1f;
			playerFireCountdown = 0f;
			playerBulletSpeed = playerSpeed * 2;
			xLimit = 3.33f;
			yLimit = 5f;

			container = new GameObject ("PlayerContainer");
			containerRB = container.AddComponent<Rigidbody2D> ();

			playerCube = Resources.Load ("PlayerCube") as GameObject;

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
			cubeWidth = cubes [0, 0].GetComponent<BoxCollider2D> ().size.x;

			sound = container.AddComponent<AudioSource> ();
			soundClip = Resources.Load ("Movement") as AudioClip;
			sound.clip = soundClip;
			sound.Play ();
			sound.Pause ();
			sound.volume = 0.25f;
			sound.loop = true;
		}

		public void fixedUpdate ()
		{
			// Setting velocity
			containerRB.velocity = Vector2.zero;
			float hDir = Input.GetAxisRaw ("Horizontal");
			float vDir = Input.GetAxisRaw ("Vertical");
			Vector2 dir = new Vector2 (hDir, vDir).normalized;
			containerRB.velocity = dir * playerSpeed;

			// Limiting movement
			Vector2 pos = container.transform.position;
			pos.x = Mathf.Clamp (pos.x, -xLimit, xLimit);
			pos.y = Mathf.Clamp (pos.y, -yLimit, yLimit);
			container.transform.position = pos;

			// Rotating
			float rotateDir = Input.GetAxisRaw ("Aim");
			containerRB.angularVelocity = 0;
			container.transform.Rotate(new Vector3(0, 0, -rotateDir * rotateSpeed * Time.deltaTime));

			// Movement sound on and off
			if (containerRB.velocity != Vector2.zero || !Mathf.Approximately(rotateDir,0f)) {
				sound.UnPause ();
			} else {
				sound.Pause ();
			}

			// Firing
			if (Input.GetButton ("Fire1") && playerFireCountdown <= 0) {
				Debug.Log("FIRE!");
				GameObject bullet = GameObject.Instantiate (Resources.Load ("PlayerBullet") as GameObject);
				bullet.transform.localScale = container.transform.localScale;
				// hacky as fuck, placing the bullet relative to the
				// container then releasing it
				bullet.transform.parent = container.transform;
				bullet.transform.rotation = container.transform.rotation;
				bullet.transform.localPosition = new Vector2 (0, cubeWidth * (cubeSide+1) / 2);
				// I seem to have to do it explicity since the .velocity is in world space
				bullet.GetComponent<Rigidbody2D> ().velocity = container.transform.up * playerBulletSpeed;
				bullet.transform.parent = null;

				UnityEngine.Object.Destroy (bullet, 20f);

				playerFireCountdown = playerFireDelay;
			}
			playerFireCountdown -= Time.deltaTime;
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