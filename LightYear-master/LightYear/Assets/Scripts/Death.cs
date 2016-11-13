using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {

	Animator animator;
	public AudioSource elephant;
	public AudioSource stomp3;
	public AudioSource stomp2;

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
			stomp2.Play ();
			StartCoroutine (reset());
			}
		if (otherObj.tag == "Boulder1") {
			Debug.Log ("collision1!");
			animator.Play ("SmallDeath");
			stomp2.Play ();
			StartCoroutine (reset());
		}
		if (otherObj.tag == "Elephant") {
			Debug.Log ("collision4!");
			animator.Play ("HugeDeath");
			elephant.Play ();
			StartCoroutine (reset());
		}
		if (otherObj.tag == "Rhino") {
			Debug.Log ("collision3!");
			animator.Play ("HugeDeath");
			stomp3.Play ();
			StartCoroutine (reset());
		}
	}

	IEnumerator reset(){
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene (0);
	}
}
