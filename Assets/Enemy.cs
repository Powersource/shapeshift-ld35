using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Enemy
	{
		GameObject enemyPrefab;
		GameObject enemyBulletPrefab;
		GameObject enemy;

		float enemyFireDelay;
		float enemyFireCountdown;

		public Enemy ()
		{
			enemyFireDelay = 2f;
			enemyFireCountdown = 0f;

			enemyPrefab = Resources.Load ("Enemy") as GameObject;
			enemyBulletPrefab = Resources.Load ("EnemyBullet") as GameObject;

			enemy = GameObject.Instantiate (enemyPrefab);
		}

		public void fixedUpdate ()
		{
			if (isAlive ()) {
				if (enemyFireCountdown <= 0) {
					GameObject enemyBullet = GameObject.Instantiate (enemyBulletPrefab);
					enemyBullet.transform.position = new Vector2 (0, 5f);
					enemyBullet.GetComponent<Rigidbody2D> ().velocity = Vector2.down;
					enemyBullet.transform.Rotate (new Vector3 (0, 0, 180f));

					enemyFireCountdown = enemyFireDelay;
				}
				enemyFireCountdown -= Time.deltaTime;
			} else {
				Debug.Log ("this object is alive but the enemy isn't");

				//TODO remove from Main somehow, I guess depending on implementation in it
			}
		}

		private bool isAlive(){
			return enemy != null;
		}
	}
}