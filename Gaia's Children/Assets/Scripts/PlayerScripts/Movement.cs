using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public float speed = 1f;

	private float Xscale;
	private float Yscale;
	private float Zscale;

	private SpriteRenderer SR;
	public Sprite Standing;
	public Sprite Moveing;
	public Sprite Fire;
	public Sprite Water;
	public Sprite Earth;

	private bool Fireing = false;
	private bool Watering = false;
	private bool AirWalking = false;
	private bool Seeding = false;
	public GameObject FireBall;
	public float fireInterval = 0.1f;
	private bool inFireSpawn = false;

	public GameObject Wind;
	public GameObject Rain;

	private AudioSource AS;
	public AudioClip FireAudio;
	public AudioClip WaterAudio;
	public AudioClip EarthAudio;


	void Awake(){
		Xscale = transform.localScale.x;
		Yscale = transform.localScale.y;
		Zscale = transform.localScale.z;
	
		SR = GetComponent<SpriteRenderer> ();
		Rain.SetActive (false);
		AS = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		CheckFire ();
		CheckWater ();
		CheckEarth ();
	}

	void Move(){
		float X = Input.GetAxisRaw ("Horizontal") * speed * Time.deltaTime;

		if (X < 0 && transform.position.x >= -8.5f && !Fireing && !Watering && !Seeding) {
			SR.sprite = Moveing;
			float XscaleTemp = -Xscale;
			transform.localScale = new Vector3(XscaleTemp, Yscale, Zscale);
			transform.Translate (new Vector3 (X, 0));
			Wind.SetActive(true);
			AirWalking = true;
		}
		if (X > 0 && transform.position.x <= 8.5f && !Fireing && !Watering && !Seeding) {
			SR.sprite = Moveing;
			float XscaleTemp = Xscale;
			transform.localScale = new Vector3(XscaleTemp, Yscale, Zscale);
			transform.Translate (new Vector3 (X, 0));
			Wind.SetActive(true);
			AirWalking = true;
		}
		if (X == 0 && !Fireing) {
			SR.sprite = Standing;
			AirWalking = false;
			Wind.SetActive(false);
		}

	}

	void CheckFire(){
		if ((Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) && !AirWalking && !Watering && !Seeding) {
			SR.sprite = Fire;
			Fireing = true;
			Wind.SetActive(false);
		}
		if ((Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) && !AirWalking && !Watering && !Seeding) {
			Vector3 BallPosition = new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z);
			Instantiate (FireBall, BallPosition, Quaternion.identity);
			AS.PlayOneShot (FireAudio);
		}
		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.UpArrow)) {
			Fireing = false;
		}
	}

	void CheckWater(){
		if (Input.GetKey (KeyCode.E) && !AirWalking && !Fireing && !Seeding) {
			SR.sprite = Water;
			Watering = true;
			Wind.SetActive(false);
			Rain.SetActive (true);;
		}
		if (Input.GetKeyDown (KeyCode.E) && !AirWalking && !Fireing && !Seeding) {
			AS.PlayOneShot (WaterAudio);
		}
		if (Input.GetKeyUp (KeyCode.E)) {
			Watering = false;
			Rain.SetActive (false);
			AS.Stop ();
		}
	}

	void CheckEarth(){
		if (Input.GetKey (KeyCode.Q) && !AirWalking && !Fireing && !Watering) {
			SR.sprite = Earth;
			Seeding = true;
			Wind.SetActive(false);
		}
		if (Input.GetKeyDown (KeyCode.Q) && !AirWalking && !Fireing && !Watering) {
			if (!AS.isPlaying) {
				AS.PlayOneShot (EarthAudio);
			}
		}
		if (Input.GetKeyUp (KeyCode.Q)) {
			Seeding = false;
		}
	}

	void OnCollisionEnter2D(Collision2D target){

	}

	void OnCollisionStay2D(Collision2D target){

	}

	void OnCollisionExit2D(Collision2D target){

	}

}//CLASS
