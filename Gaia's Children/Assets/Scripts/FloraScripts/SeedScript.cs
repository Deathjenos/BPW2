using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedScript : MonoBehaviour {

	public float speed = 1f;
	public float DeleteTime = 10f;

	private bool onGround = false;
	private bool inDelete = false;

	public bool withPlayer = false;

	public GameObject growAngelicFruit;
	public float spawnFruitY = -0.36f;
	public float spawnFruitZ = -1f;

	public float rotationSpeedMin = 0.4f;
	public float rotationSpeedMax = 0.6f;

	private Animator Anim;

	void Awake(){
		Anim = GetComponent<Animator> ();
		float randomRotationSpeed = Random.Range (rotationSpeedMin, rotationSpeedMax);
		Anim.speed = randomRotationSpeed;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move(){
		if (!onGround) {
			float Y = -speed * Time.deltaTime;

			transform.position = new Vector3 (transform.position.x, transform.position.y + Y, transform.position.z);
		}
		if (onGround) {
			Anim.speed = 0;
			if (!inDelete) {
				StartCoroutine (Deletion ());
			}
			if (Input.GetKeyDown (KeyCode.Q) && withPlayer) {
				Vector3 GrowFruitPos = new Vector3 (transform.position.x, spawnFruitY, spawnFruitZ);
				Instantiate (growAngelicFruit, GrowFruitPos, Quaternion.identity);
				Destroy (gameObject);
			}
		}
	}

	IEnumerator Deletion(){
		inDelete = true;
		yield return new WaitForSeconds (DeleteTime);
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D Target){
		if (Target.gameObject.tag == "Ground") {
			onGround = true;
		}
		if (Target.gameObject.tag == "Player") {
			withPlayer = true;
		}
	}
	void OnTriggerStay2D(Collider2D Target){
		if (Target.gameObject.tag == "Ground") {
			onGround = true;
		}
		if (Target.gameObject.tag == "Player") {
			withPlayer = true;
		}
	}
	void OnTriggerExit2D(Collider2D Target){
		if (Target.gameObject.tag == "Ground") {
			onGround = false;
		}
		if (Target.gameObject.tag == "Player") {
			withPlayer = false;
		}
	}

}//CLASS
