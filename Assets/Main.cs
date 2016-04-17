using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
	GameObject simpleEnemy;
	GameObject deadCanvas;

	Player player;

	// Use this for initialization
	void Start ()
	{
		Time.timeScale = 1.0f;
		deadCanvas = GameObject.Find ("DeadCanvas");
		deadCanvas.SetActive (false);
		player = new Player ();
		simpleEnemy = Resources.Load ("SimpleEnemy") as GameObject;
		GameObject enemy = GameObject.Instantiate (simpleEnemy);
		enemy.transform.position = new Vector2 (0, 5f);
		enemy.GetComponent<Rigidbody2D> ().velocity = Vector2.down;
		enemy.transform.Rotate (new Vector3 (0, 0, 180f));
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate ()
	{
		player.fixedUpdate ();
		if (Input.GetButton ("Jump")) {
			Time.timeScale = 0.25f;
		} else {
			Time.timeScale = 1.0f;
		}
	}

	public void hurt (GameObject cube, Collision2D collision)
	{
		if (collision.gameObject.tag == "hurt") {
			cube.SetActive (false);
			if (!player.isAlive ()) {
				gameOver ();
			}
		}
	}

	public void bulletHurt (GameObject bullet, Collision2D collision)
	{
		if (collision.gameObject.tag == "enemy") {
			Debug.Log ("Hit enemy. Killing enemy and bullet.");
			Object.Destroy (collision.gameObject);
			Object.Destroy (bullet);
		} else if (collision.gameObject.tag.Equals ("hurt")) {
			Debug.Log ("Hit enemy bullet. Killing both bullets.");
			Object.Destroy (collision.gameObject);
			Object.Destroy (bullet);
		} else {
			Debug.LogWarning ("Some weird collision just happened");
		}
	}

	void gameOver ()
	{
		Time.timeScale = 0;
		deadCanvas.SetActive (true);
	}

	public void restart ()
	{
		Debug.Log ("Restart");
		SceneManager.LoadScene ("scene0");
	}
}