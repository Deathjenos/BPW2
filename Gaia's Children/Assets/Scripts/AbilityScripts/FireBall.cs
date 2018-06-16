using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

	public float speed = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move(){
		float Y = speed * Time.deltaTime;

		transform.Translate (new Vector2 (0, Y));
	}

	void OnTriggerEnter2D(Collider2D Target){
		if (Target.gameObject.tag == "Alien") {
			Destroy (Target.gameObject);
			Destroy (gameObject);
		}
	}

}//CLASS
