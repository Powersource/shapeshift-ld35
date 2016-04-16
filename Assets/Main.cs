using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Main : MonoBehaviour
{
	public GameObject playerCube;
	public GameObject simpleEnemy;

	Player player;

	// Use this for initialization
	void Start ()
	{
		player = new Player (playerCube);
		GameObject enemy = GameObject.Instantiate (simpleEnemy);//, new Vector3(0,-10,0),Quaternion.identity);
		enemy.transform.position = new Vector2 (-5f, 0);
		enemy.GetComponent<Rigidbody2D> ().velocity = Vector2.right;
		enemy.transform.rotation = Quaternion.LookRotation (new Vector3 (0, 0, 1));
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate ()
	{
		player.move ();
	}

	public void hurt(GameObject cube, Collision2D collision){
		if (collision.gameObject.tag == "hurt") {
			cube.SetActive(false);
			if (!player.isAlive ()) {
				gameOver ();
			}
		}
	}

	void gameOver (){
		Debug.Log ("DEAD");
	}
}