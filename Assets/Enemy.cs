using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Enemy
	{
		GameObject enemyPrefab;
		GameObject enemyBulletPrefab;
		public GameObject enemy;
		GameObject player;

		float enemyFireDelay;
		float enemyFireCountdown;
		float enemyBulletSpeed;
		float enemyRotationSpeed;
		float enemyAge;
		float enemyXVel;
		float enemyXAmp;
		float enemyYVel;

		public Enemy ()
		{
			enemyFireDelay = UnityEngine.Random.Range (1.25f, 1.75f);
			enemyFireCountdown = 0f;
			enemyXVel = UnityEngine.Random.Range (-6f, 6f);
			enemyXAmp = UnityEngine.Random.Range (-3f, 3f);
			enemyYVel = UnityEngine.Random.Range (0.5f, 2.5f);
			enemyBulletSpeed = enemyYVel * 2;
			enemyRotationSpeed = 30f;

			enemyPrefab = Resources.Load ("Enemy") as GameObject;
			enemyBulletPrefab = Resources.Load ("EnemyBullet") as GameObject;

			enemy = GameObject.Instantiate (enemyPrefab);
			enemy.transform.position = new Vector2 (UnityEngine.Random.Range (-3.33f, 3.33f), 5f);
			enemy.transform.rotation = Quaternion.AngleAxis(180f,Vector3.forward);
			enemy.transform.localScale /= 2;

			player = GameObject.Find ("PlayerContainer");

			enemyAge = 0f;
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
					enemyBullet.transform.localScale = enemy.transform.localScale;
					enemyBullet.transform.parent = null;

					UnityEngine.Object.Destroy (enemyBullet, 20f);

					enemyFireCountdown = enemyFireDelay;
				}

				// Maybe copy paste
				Vector3 vectorToTarget = player.transform.position - enemy.transform.position;
				float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90f;
				Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
				enemy.transform.rotation = Quaternion.RotateTowards (enemy.transform.rotation, q, Time.deltaTime * enemyRotationSpeed);
				//Quaternion.Slerp(enemy.transform.rotation, q, Time.deltaTime * enemyRotationSpeed);

				enemyAge += Time.deltaTime;
				enemy.GetComponent<Rigidbody2D> ().velocity = new Vector2 (enemyXAmp * Mathf.Sin (enemyAge * enemyXVel), -enemyYVel);

				enemyFireCountdown -= Time.deltaTime;
			} else {
				Debug.Log ("this object is alive but the enemy isn't");

				// TODO remove from Main somehow, I guess depending on implementation in it
				// or maybe i don't have to do this, idk
			}
		}

		public bool isAlive ()
		{
			return enemy != null;
		}
	}
}