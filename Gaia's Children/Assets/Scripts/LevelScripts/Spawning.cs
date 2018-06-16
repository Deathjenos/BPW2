using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour {

	public float SpawnTime = 5f;

	public float MaxX = 8f;
	public float MinX = -8f;
	public float MaxY = 8f;
	public float MinY = -8f;
	public float Z = -2;

	public int MaxObjects = 10;

	public GameObject Object;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnObject ());
	}
	
	// Update is called once per frame
	void Update () {
		DeleteOverflow ();
	}

	IEnumerator SpawnObject(){
		float randomX = Random.Range (MinX, MaxX);
		float randomY = Random.Range (MinY, MaxY);

		Instantiate(Object, new Vector3(randomX, randomY, Z), Quaternion.identity);
			
		yield return new WaitForSeconds (SpawnTime);
		
		StartCoroutine (SpawnObject());
	}

	void DeleteOverflow(){
		GameObject[] ObjectList = GameObject.FindGameObjectsWithTag (Object.gameObject.tag);

		if(ObjectList.Length > MaxObjects){
			Destroy (ObjectList [0].gameObject);
		}
	}

}//CLASS
