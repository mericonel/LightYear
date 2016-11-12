using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider otherObj){

		if (otherObj.tag == "Boulder1" || otherObj.tag == "Boulder2" || otherObj.tag == "Rhino" || otherObj.tag == "Elephant"){
			Destroy(otherObj.gameObject); 
		}
	}
}
