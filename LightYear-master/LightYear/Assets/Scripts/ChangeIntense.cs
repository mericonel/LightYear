using UnityEngine;
using System.Collections;

public class ChangeIntense : MonoBehaviour {

	Animator animator;
	public AudioSource fire;

	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {

	}

		void OnTriggerEnter (Collider otherObj){

		if (otherObj.tag == "Wood") {
			Destroy (otherObj.gameObject);
			Debug.Log ("Light Activate!");
			animator.SetTrigger ("LightIntensity");
			fire.Play ();
		}
	
	}
}
