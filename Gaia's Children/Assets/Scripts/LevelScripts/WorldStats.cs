using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStats : MonoBehaviour {

	public GameObject GaiaBar;
	private Bar GaiaBarScript;
	public float CurrentGaia = 0f;
	public float MaxGaia = 100f;

	public GameObject WinBar;

	void Awake(){
		GaiaBarScript = GaiaBar.GetComponent<Bar> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckBars ();
		CheckWinLose ();
	}

	void CheckBars(){
		//Check Gaia
		float Procent = (CurrentGaia / MaxGaia) * 100f;
		if (CurrentGaia >= MaxGaia) {
			CurrentGaia = MaxGaia;
		}
		GaiaBarScript.currentprocent = Procent;
	}

	void CheckWinLose(){
		//Check if won
		if (CurrentGaia >= MaxGaia) {
			WinBar.SetActive (true);
		} 
		else {
			WinBar.SetActive (false);
		}
	}

}//CLASS
