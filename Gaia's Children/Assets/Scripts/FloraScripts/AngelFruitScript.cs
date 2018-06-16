using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelFruitScript : MonoBehaviour {

	public float WaterLevel = 80f;
	public float NormalWaterLevel = 100f;
	public float LowWaterLevel = 30f;
	public float DeathLowWaterLevel = 10f;
	public float HighWaterLevel = 130f;
	public float DeathHighWaterLevel = 150f;
	public float WaterDecreaseTime = 2f;
	public float WaterDecreaseAmount = 5f;
	public float AlienWaterDecreaseAmount = 10f;
	public float WateringAmount = 10f;
	public float WateringPerSecond = 1f;
	private bool inWatering = false;
	private bool inWaterDecrease = false;

	public float CurrentBerry = 0f;
	public float MaxBerryGeneration = 100f;
	public float BerryGenerationTime = 1f;
	public float BerryGenerationAmount = 5f;
	public float CurrentGenerationAmount;
	private bool inBerryGeneration = false;
	public bool hasBerries = false;

	private SpriteRenderer SR;
	public Sprite NormalWaterFruit;
	public Sprite DeathLowWaterFruit;
	public Sprite LowWaterFruit;
	public Sprite DeathHighWaterFruit;
	public Sprite HighWaterFruit;

	public Sprite NormalWithFruit;
	public Sprite DeathLowWithFruit;
	public Sprite LowWithFruit;
	public Sprite DeathHighWithFruit;
	public Sprite HighWithFruit;

	private float PlantState;

	private bool withPlayer = false;
	private GameObject Alien;
	private bool withAlien = false;

	public GameObject WorldStatsObject;
	private WorldStats WS;

	void Awake(){
		SR = GetComponent<SpriteRenderer> ();

		CurrentGenerationAmount = BerryGenerationAmount;
		WorldStatsObject = GameObject.Find ("WorldStats");
		WS = WorldStatsObject.GetComponent<WorldStats> ();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckWater ();
		CheckBerries ();
	}

	void CheckWater(){
		//Death
		if (WaterLevel <= 0) {
			Destroy (gameObject);
		}

		//Death Low Water
		if (WaterLevel > 0 && WaterLevel <= DeathLowWaterLevel) {
			if (hasBerries) {
				SR.sprite = DeathLowWithFruit;
			} 
			else {
				SR.sprite = DeathLowWaterFruit;
			}
			CurrentGenerationAmount = BerryGenerationAmount - 2;
			PlantState = 1;
		}

		//Low Water
		if (WaterLevel > DeathLowWaterLevel && WaterLevel <= LowWaterLevel) {
			if (hasBerries) {
				SR.sprite = LowWithFruit;
			} 
			else {
				SR.sprite = LowWaterFruit;
			}
			CurrentGenerationAmount = BerryGenerationAmount - 1;
			PlantState = 2;
		}

		//Normal Water
		if (WaterLevel > LowWaterLevel && WaterLevel <= NormalWaterLevel) {
			if (hasBerries) {
				SR.sprite = NormalWithFruit;
			} 
			else {
				SR.sprite = NormalWaterFruit;
			}
			CurrentGenerationAmount = BerryGenerationAmount;
			PlantState = 3;
		}

		//High Water
		if (WaterLevel > NormalWaterLevel && WaterLevel <= HighWaterLevel) {
			if (hasBerries) {
				SR.sprite = HighWithFruit;
			} 
			else {
				SR.sprite = HighWaterFruit;
			}
			CurrentGenerationAmount = BerryGenerationAmount + 1;
			PlantState = 4;
		}

		//Death High Water
		if (WaterLevel > HighWaterLevel && WaterLevel <= DeathHighWaterLevel) {
			if (hasBerries) {
				SR.sprite = DeathHighWithFruit;
			} 
			else {
				SR.sprite = DeathHighWaterFruit;
			}
			CurrentGenerationAmount = BerryGenerationAmount + 2;
			PlantState = 5;
		}

		//Death
		if (WaterLevel > DeathHighWaterLevel) {
			Destroy (gameObject);
		}

		//Decrease Water
		if (!inWaterDecrease) {
			StartCoroutine (WaterDecrease ());
		}

		//Add Water
		if (Input.GetKey (KeyCode.E) && withPlayer) {
			if (!inWatering) {
				WaterLevel += (WateringAmount/60);
				StartCoroutine (Watering ());
			}
		}

		//Check Alien
		if (Alien == null) {
			withAlien = false;
		}

	}

	IEnumerator WaterDecrease(){
		inWaterDecrease = true;
		yield return new WaitForSeconds ((WaterDecreaseTime/12));
		if (withAlien) {
			WaterLevel -= (AlienWaterDecreaseAmount/12);
		}
		if (!withAlien) {
			WaterLevel -= (WaterDecreaseAmount/12);
		}
		inWaterDecrease = false;
	}

	IEnumerator Watering(){
		inWatering = true;
		yield return new WaitForSeconds ((WateringPerSecond/60));
		inWatering = false;
	}

	void CheckBerries(){
		//Increase Berries
		if (!inBerryGeneration && CurrentBerry < MaxBerryGeneration) {
			StartCoroutine (BerryGeneration ());
		}

		//Check Has berries
		if (CurrentBerry >= MaxBerryGeneration) {
			hasBerries = true;
		}
		if (CurrentBerry < MaxBerryGeneration) {
			hasBerries = false;
		}

		//Get Berries
		if (hasBerries && Input.GetKeyDown(KeyCode.Q) && withPlayer) {
			hasBerries = false;
			CurrentBerry = 0;
			if (PlantState == 1) {
				WS.CurrentGaia += 1;
			} 
			else if (PlantState == 2) {
				WS.CurrentGaia += 2;
			}
			else if (PlantState == 3) {
				WS.CurrentGaia += 5;
			}
			else if (PlantState == 4) {
				WS.CurrentGaia += 2;
			}
			else if (PlantState == 5) {
				WS.CurrentGaia += 1;
			}
		}
	}

	IEnumerator BerryGeneration(){
		inBerryGeneration = true;
		yield return new WaitForSeconds (BerryGenerationTime);
		CurrentBerry += CurrentGenerationAmount;
		inBerryGeneration = false;
	}

	void OnTriggerEnter2D(Collider2D Target){
		if (Target.gameObject.tag == "Player") {
			withPlayer = true;
		}
		if (Target.gameObject.tag == "AlienLaser") {
			withAlien= true;
		}
	}
	void OnTriggerStay2D(Collider2D Target){
		if (Target.gameObject.tag == "Player") {
			withPlayer = true;
		}
		if (Target.gameObject.tag == "AlienLaser") {
			withAlien= true;
			Alien = Target.gameObject;
		}
	}
	void OnTriggerExit2D(Collider2D Target){
		if (Target.gameObject.tag == "Player") {
			withPlayer = false;
		}
		if (Target.gameObject.tag == "AlienLaser") {
			withAlien= false;
		}
	}

}//CLASS
