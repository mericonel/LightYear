using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private float speed = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	transform.position += Vector3.forward * speed * Time.deltaTime;

		if (Input.GetKey ("left")) {
			transform.position += Vector3.left * Time.deltaTime * 5;
		}
		if (Input.GetKey ("right")) {
			transform.position += Vector3.right * Time.deltaTime * 5;
		}

	
	}
}
