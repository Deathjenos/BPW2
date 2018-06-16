using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {

	public float currentprocent = 100f;

	private float BarWidth;
	private float BarHeight;

	private float X;
	private float Y;

	private RectTransform RT;

	void Awake(){
		RT = GetComponent<RectTransform> ();
		BarWidth = RT.sizeDelta.x;
		BarHeight = RT.sizeDelta.y;
		X = RT.localPosition.x;
		Y = RT.localPosition.y;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckBar ();
	}

	void CheckBar(){

		float BarCurrentWidth = BarWidth * (currentprocent / 100);

		RT.sizeDelta = new Vector2 (BarCurrentWidth , BarHeight);

		float AlignLeft = ((BarWidth - BarCurrentWidth) / 2); 
		RT.localPosition = new Vector3 ((X - AlignLeft), Y, RT.localPosition.z);
	}

}//CLASS
