using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Main : MonoBehaviour
{
	public GameObject playerCube;

	Player player;

	// Use this for initialization
	void Start ()
	{
		player = new Player(playerCube);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate ()
	{
		
	}
}
