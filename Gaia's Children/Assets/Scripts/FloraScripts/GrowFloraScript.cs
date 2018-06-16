using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowFloraScript : MonoBehaviour {

	public float AnimationTime = 0.3f;
	public GameObject AngelicFruit;

	// Use this for initialization
	void Start () {
		StartCoroutine (ChangeObject ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator ChangeObject(){
		yield return new WaitForSeconds (AnimationTime);
		Instantiate (AngelicFruit, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
