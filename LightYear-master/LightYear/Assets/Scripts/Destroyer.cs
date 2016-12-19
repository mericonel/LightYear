using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Destroyer : MonoBehaviour {

	public GameObject upleft10;
	public GameObject upright10;
	public GameObject upleft5;
	public GameObject upright5;

	public GameObject mainObj;

	public AudioSource pointSound;

	public GameObject scoreboard;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	void OnTriggerEnter (Collider otherObj){

		if (otherObj.tag == "Boulder1" || otherObj.tag == "Boulder2") {
			if (otherObj.transform.position.x < mainObj.transform.position.x) {
				upleft5.GetComponent<Animator> ().Play ("pointAnim");
				Destroy (otherObj.gameObject); 
				pointSound.Play ();
				scoreboard.GetComponent<ScoreTracker> ().addScore (5);
			} else if (otherObj.transform.position.x >= mainObj.transform.position.x) {
				upright5.GetComponent<Animator> ().Play ("pointAnim");
				Destroy (otherObj.gameObject);
				pointSound.Play ();
				scoreboard.GetComponent<ScoreTracker> ().addScore (5);
			}
		}
		if (otherObj.tag == "Rhino" || otherObj.tag == "Elephant") {
			if (otherObj.transform.position.x < mainObj.transform.position.x) {
				upleft10.GetComponent<Animator> ().Play ("pointAnim");
				Destroy (otherObj.gameObject); 
				pointSound.Play ();
				scoreboard.GetComponent<ScoreTracker> ().addScore (10);
			} else if (otherObj.transform.position.x >= mainObj.transform.position.x) {
				upright10.GetComponent<Animator> ().Play ("pointAnim");
				Destroy (otherObj.gameObject);
				pointSound.Play ();
				scoreboard.GetComponent<ScoreTracker> ().addScore (10);
			}
		}

	}
}
