using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Death : MonoBehaviour {

	Animator animator;
	public AudioSource elephant;
	public AudioSource stomp3;
	public AudioSource stomp2;

	public Canvas canvas;

	public GameObject mainObj;
	public GameObject mainCamera;
	public GameObject SpawnerToDestroy1;
	public GameObject SpawnerToDestroy2;
	public GameObject fader;
	public GameObject torch;
	public GameObject musicPlayer;
	public GameObject restarter;
	public GameObject destroyer;

	public GameObject totem1;
	public GameObject totem2;
	public GameObject fireworks1;
	public GameObject fireworks2;

	public AudioSource frost;
	public AudioSource totemDeath;

	public GameObject terraingroup;

	bool canYouReset = false;
	public bool deathOccured = false;


	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (canYouReset == true) {
			if (Input.GetKey (KeyCode.Escape)) {
				SceneManager.LoadScene (0);
			}

			if (Input.GetButton ("BButton")) {
				SceneManager.LoadScene (0);
			}

			if (Input.GetKey (KeyCode.Return)) {
				SceneManager.LoadScene (1);
			}
			if (Input.GetButton ("AButton")) {
				SceneManager.LoadScene (1);	
			}	
		} 
	}


	void OnTriggerEnter (Collider otherObj){

		if (otherObj.tag == "Boulder2") {
			Debug.Log ("collision2!");
			animator.Play ("BigDeath");
			GetComponent<BoxCollider> ().enabled = false;
			Destroy (mainObj.GetComponent<Movement> ());
			Destroy (destroyer.gameObject);
			mainCamera.GetComponent<Animator> ().Play ("Frost");
			frost.Play ();
			canvas.enabled = false;
			deathOccured = true;
			stomp2.Play ();
			StartCoroutine (reset ());
			StartCoroutine (fireworks ());
		}
		if (otherObj.tag == "Boulder1") {
			Debug.Log ("collision1!");
			animator.Play ("SmallDeath");
			GetComponent<BoxCollider> ().enabled = false;
			Destroy (mainObj.GetComponent<Movement> ());
			Destroy (destroyer.gameObject);
			mainCamera.GetComponent<Animator> ().Play ("Frost");
			frost.Play ();
			canvas.enabled = false;
			deathOccured = true;
			stomp2.Play ();
			StartCoroutine (reset ());
			StartCoroutine (fireworks ());
		}
		if (otherObj.tag == "Elephant") {
			Debug.Log ("collision4!");
			animator.Play ("HugeDeath");
			GetComponent<BoxCollider> ().enabled = false;
			Destroy (mainObj.GetComponent<Movement> ());
			Destroy (destroyer.gameObject);
			mainCamera.GetComponent<Animator> ().Play ("Frost");
			frost.Play ();
			canvas.enabled = false;
			deathOccured = true;
			elephant.Play ();
			StartCoroutine (reset ());
			StartCoroutine (fireworks ());
		}
		if (otherObj.tag == "Rhino") {
			Debug.Log ("collision3!");
			animator.Play ("HugeDeath");
			GetComponent<BoxCollider> ().enabled = false;
			Destroy (mainObj.GetComponent<Movement> ());
			Destroy (destroyer.gameObject);
			mainCamera.GetComponent<Animator> ().Play ("Frost");
			frost.Play ();
			canvas.enabled = false;
			deathOccured = true;
			stomp3.Play ();
			StartCoroutine (reset ());
			StartCoroutine (fireworks ());
		}

		if (otherObj.tag == "TerrainSpawner") {
			terraingroup.SetActive (true);
		}
	}

	IEnumerator reset(){
		Time.timeScale = 1;
		yield return new WaitForSeconds (3);
		restarter.SetActive (true);
		canYouReset = true;
	}

	IEnumerator fireworks(){
		fireworks1.SetActive (true);
		fireworks2.SetActive (true);
		yield return new WaitForSeconds (1);
		totemDeath.Play ();
		totem1.SetActive (false);
		totem2.SetActive (false);
		yield return new WaitForSeconds (1.4f);
		fireworks1.SetActive (false);
		fireworks2.SetActive (false);
	}
}
