using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
	public GameObject playerCube;
	public GameObject simpleEnemy;

	GameObject deadCanvas;

	Player player;

	// Use this for initialization
	void Start ()
	{
		Time.timeScale = 1.0f;
		deadCanvas = GameObject.Find ("DeadCanvas");
		deadCanvas.SetActive (false);
		player = new Player (playerCube);
		GameObject enemy = GameObject.Instantiate (simpleEnemy);
		enemy.transform.position = new Vector2 (0, 10f);
		enemy.GetComponent<Rigidbody2D> ().velocity = Vector2.down;
		enemy.transform.Rotate(new Vector3(0,0,180f));
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate ()
	{
		player.move ();
		if (Input.GetButton ("Jump")) {
			Time.timeScale = 0.25f;
		} else {
			Time.timeScale = 1.0f;
		}
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
//		Text text = deadCanvas.AddComponent<Text> ();
//		text.text = "You dead";
//		// I'm setting the "size" to 12 but the font size ends up being 14
//		text.font = UnityEngine.Font.CreateDynamicFontFromOSFont("Arial", 12);
//		Button button = deadCanvas.AddComponent<Button> ();
		Time.timeScale = 0;
		deadCanvas.SetActive(true);
	}

	public void restart(){
		Debug.Log("Restart");
		SceneManager.LoadScene ("scene0");
	}
}