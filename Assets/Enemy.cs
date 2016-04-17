using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Enemy
	{
		GameObject enemy;

		public Enemy ()
		{
			enemy = Resources.Load ("Enemy") as GameObject;

			GameObject enemyBullet = GameObject.Instantiate (simpleEnemy);
			enemyBullet.transform.position = new Vector2 (0, 5f);
			enemyBullet.GetComponent<Rigidbody2D> ().velocity = Vector2.down;
			enemyBullet.transform.Rotate (new Vector3 (0, 0, 180f));
		}

		public void fixedUpdate(){
			
		}
	}
}

