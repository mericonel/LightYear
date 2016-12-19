using UnityEngine;
using System.Collections;

public class RockMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += Vector3.back * 4 * Time.deltaTime;
	
	}
}
