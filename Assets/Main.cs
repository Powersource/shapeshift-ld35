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
	ArrayList enemies;

	float timeUntilEnemy;

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
		enemies = new ArrayList ();

		timeUntilEnemy = 1f;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate ()
	{
		player.fixedUpdate ();
		foreach (Enemy enemy in enemies) { 
			// This throws an exception but I'd argue less breaks because
			// of it so I'm keeping it.
			if (!enemy.isAlive() || enemy.enemy.transform.position.y < -6f) {
				enemies.Remove (enemy);
			}
			enemy.fixedUpdate ();
		}

//		if (Input.GetButton ("Jump")) {
//			Time.timeScale = 0.25f;
//		} else {
//			Time.timeScale = 1.0f;
//		}

		if (Input.GetButton ("Jump")) {
			gameOver ();
		}

		if (timeUntilEnemy <= 0) {
			enemies.Add(new Enemy ());
			timeUntilEnemy = enemySpawnDelay ();
			Debug.Log ("Time to next spawn: " + timeUntilEnemy);
		}
		timeUntilEnemy -= Time.deltaTime;
	}

	private float enemySpawnDelay(){
		return 4 * Mathf.Pow (1.02f, -Time.timeSinceLevelLoad);
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
		if (collision.gameObject.tag.Equals ("enemy")) {
			Debug.Log ("Hit enemy. Killing enemy and bullet.");
			Object.Destroy (collision.gameObject);
			Object.Destroy (bullet);
			new Explosion ();
		} else if (collision.gameObject.tag.Equals ("hurt")) {
			Debug.Log ("Hit enemy bullet. Killing both bullets.");
			Object.Destroy (collision.gameObject);
			Object.Destroy (bullet);
		} else {
			Debug.LogWarning (bullet.gameObject.tag + " just collided with " + collision.gameObject.tag);
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