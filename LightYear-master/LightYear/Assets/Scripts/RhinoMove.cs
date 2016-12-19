using UnityEngine;
using System.Collections;

public class RhinoMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += Vector3.back * 7 * Time.deltaTime;
	
	}
}
