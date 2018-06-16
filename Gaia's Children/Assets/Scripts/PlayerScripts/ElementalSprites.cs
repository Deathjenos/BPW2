using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalSprites : MonoBehaviour {

	public Sprite Water;
	public Sprite Fire;
	public Sprite Air;
	public Sprite Earth;

	private SpriteRenderer SR;

	void Awake(){
		SR = GetComponent<SpriteRenderer> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ChangeElement ();
	}

	void ChangeElement(){
		//Set Fire image
		if (Input.GetKey (KeyCode.Alpha1)) {
			SR.sprite = Fire;
		}
		//Set Water image
		if (Input.GetKey (KeyCode.Alpha2)) {
			SR.sprite = Water;
		}
		//Set Air image
		if (Input.GetKey (KeyCode.Alpha3)) {
			SR.sprite = Air;
		}
		//Set Earth image
		if (Input.GetKey (KeyCode.Alpha4)) {
			SR.sprite = Earth;
		}
	}

}//CLASS
