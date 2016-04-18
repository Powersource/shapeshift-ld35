using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Enemy
	{
		GameObject enemyPrefab;
		GameObject enemyBulletPrefab;
		GameObject enemy;
		GameObject player;

		float enemyFireDelay;
		float enemyFireCountdown;
		float enemySpeed;
		float enemyBulletSpeed;
		float enemyRotationSpeed;

		public Enemy ()
		{
			enemyFireDelay = 2f;
			enemyFireCountdown = 0f;
			enemySpeed = 2.5f;
			enemyBulletSpeed = enemySpeed * 2;
			enemyRotationSpeed = 30f;

			enemyPrefab = Resources.Load ("Enemy") as GameObject;
			enemyBulletPrefab = Resources.Load ("EnemyBullet") as GameObject;

			enemy = GameObject.Instantiate (enemyPrefab);

			player = GameObject.Find ("PlayerContainer");
		}

		public void fixedUpdate ()
		{
			if (isAlive ()) {
				if (enemyFireCountdown <= 0) {
					GameObject enemyBullet = GameObject.Instantiate (enemyBulletPrefab);
					enemyBullet.transform.parent = enemy.transform;
					enemyBullet.transform.localPosition = new Vector2 (0, 1f);
					enemyBullet.GetComponent<Rigidbody2D> ().velocity = enemy.transform.up * enemyBulletSpeed;
					enemyBullet.transform.rotation = enemy.transform.rotation;
					enemyBullet.transform.parent = null;

					UnityEngine.Object.Destroy (enemyBullet, 20f);

					enemyFireCountdown = enemyFireDelay;
				}

				// Maybe copy paste
				Vector3 vectorToTarget = player.transform.position - enemy.transform.position;
				float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90f;
				Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
				enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, q, Time.deltaTime*enemyRotationSpeed);
				//Quaternion.Slerp(enemy.transform.rotation, q, Time.deltaTime * enemyRotationSpeed);

				enemyFireCountdown -= Time.deltaTime;
			} else {
				//Debug.Log ("this object is alive but the enemy isn't");

				// TODO remove from Main somehow, I guess depending on implementation in it
				// or maybe i don't have to do this, idk
			}
		}

		private bool isAlive(){
			return enemy != null;
		}
	}
}