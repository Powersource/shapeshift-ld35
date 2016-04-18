using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
	GameObject deadCanvas;
	GameObject musicPrefab;
	GameObject music;

	Player player;
	Enemy enemy;

	// Use this for initialization
	void Start ()
	{
		Time.timeScale = 1.0f;
		deadCanvas = GameObject.Find ("DeadCanvas");
		deadCanvas.SetActive (false);

		musicPrefab = Resources.Load ("Music") as GameObject;
		music = GameObject.Find ("Music");
		// If we've just started the game and don't have a music object since before
		if (music == null) {
			music = GameObject.Instantiate (musicPrefab);
			music.name = "Music";
			Object.DontDestroyOnLoad (music);
		}
		//music.GetComponent<AudioSource> ().mute = true;

		player = new Player ();
		enemy = new Enemy ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate ()
	{
		player.fixedUpdate ();
		enemy.fixedUpdate ();

//		if (Input.GetButton ("Jump")) {
//			Time.timeScale = 0.25f;
//		} else {
//			Time.timeScale = 1.0f;
//		}

		if (Input.GetButton ("Jump")) {
			gameOver ();
		}
	}

	public void hurt (GameObject cube, Collision2D collision)
	{
		if (collision.gameObject.tag == "hurt") {
			cube.SetActive (false);
			Object.Destroy (collision.gameObject);
			new Explosion ();
			if (!player.isAlive ()) {
				gameOver ();
			}
		}
	}

	public void bulletHurt (GameObject bullet, Collision2D collision)
	{
		if (collision.gameObject.tag.Equals("enemy")) {
			Debug.Log ("Hit enemy. Killing enemy and bullet.");
			Object.Destroy (collision.gameObject);
			Object.Destroy (bullet);
			new Explosion ();
		} else if (collision.gameObject.tag.Equals ("hurt")) {
			Debug.Log ("Hit enemy bullet. Killing both bullets.");
			Object.Destroy (collision.gameObject);
			Object.Destroy (bullet);
		} else {
			Debug.LogWarning (bullet.gameObject.tag + " just collided with " + collision.gameObject.tag );
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