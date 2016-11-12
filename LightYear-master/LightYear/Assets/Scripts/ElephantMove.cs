using UnityEngine;
using System.Collections;

public class ElephantMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += Vector3.back * 8 * Time.deltaTime;
	
	}
}
