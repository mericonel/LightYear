using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter (Collider otherObj){

		if (otherObj.tag == "Boulder2"){
			Debug.Log ("collision2!");
				animator.Play ("BigDeath");
			StartCoroutine (reset());
			}
		if (otherObj.tag == "Boulder1") {
			Debug.Log ("collision1!");
			animator.Play ("SmallDeath");
			StartCoroutine (reset());
		}
	}

	IEnumerator reset(){
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene (0);

	}
}
