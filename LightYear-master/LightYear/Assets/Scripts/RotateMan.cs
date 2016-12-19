using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RotateMan : MonoBehaviour {

	public Slider slider;

	float z;

	// Use this for initialization
	void Start () {
	
		StartCoroutine (rotate ());
	}
	
	// Update is called once per frame
	void Update () {

		z = transform.rotation.z;
	
	}

	IEnumerator rotate (){
		yield return new WaitForSeconds (5);
		slider.value = z * 2;

	}
}
