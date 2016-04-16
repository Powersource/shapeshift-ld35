using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
	public GameObject playerCube;
	public GameObject simpleEnemy;

	GameObject canvas;

	Player player;

	// Use this for initialization
	void Start ()
	{
		canvas = GameObject.Find ("DeadCanvas");
		canvas.SetActive (false);
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
//		Text text = canvas.AddComponent<Text> ();
//		text.text = "You dead";
//		// I'm setting the "size" to 12 but the font size ends up being 14
//		text.font = UnityEngine.Font.CreateDynamicFontFromOSFont("Arial", 12);
//		Button button = canvas.AddComponent<Button> ();
		Time.timeScale = 0;
		canvas.SetActive(true);
	}

	public void restart(){
		Debug.Log("Restart");
	}
}