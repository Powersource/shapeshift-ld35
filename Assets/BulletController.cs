using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	GameObject controller;

	// Use this for initialization
	void Start () {
		controller = GameObject.Find ("Controller");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision){
		controller.GetComponent<Main>().bulletHurt (this.gameObject, collision);
	}
}
