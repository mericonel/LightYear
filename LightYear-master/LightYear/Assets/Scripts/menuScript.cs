using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class menuScript : MonoBehaviour {

	public EventSystem ES;
	private GameObject StoreSelected;

	public Canvas startMenu;
	public Canvas aboutMenu;
	public Button about;
	public Button back;
	public Button begin;
	public Button exit;

	public GameObject fader;
	public GameObject mainCamera;

	public GameObject background;


	// Use this for initialization
	void Start () {

		aboutMenu = aboutMenu.GetComponent<Canvas> ();
		about = about.GetComponent<Button> ();
		back = back.GetComponent<Button> ();
		begin = begin.GetComponent<Button> ();
		exit = exit.GetComponent<Button> ();
		aboutMenu.enabled = false;
		back.enabled = false;

		StoreSelected = ES.firstSelectedGameObject;

	}

	void Update (){

		if (aboutMenu.enabled == true) {
			if (Input.GetButton ("AButton")) {
				StartCoroutine (backPressed ());
				aboutMenu.enabled = false;
				back.enabled = false;
				mainCamera.GetComponent<Animator> ().Play ("zoomOutInfo");
			}
		}

		if (aboutMenu.enabled == true) {
			if (Input.GetKey (KeyCode.Return)) {
				StartCoroutine (backPressed ());
				aboutMenu.enabled = false;
				back.enabled = false;
				mainCamera.GetComponent<Animator> ().Play ("zoomOutInfo");
			}
		}

		if (ES.currentSelectedGameObject != StoreSelected) {
			if (ES.currentSelectedGameObject == null) {
				ES.SetSelectedGameObject (StoreSelected);
			} else {
				StoreSelected = ES.currentSelectedGameObject;
			}
		}
	}

	public void aboutPress(){

		StartCoroutine (aboutPressed ());
		startMenu.enabled = false;
		begin.enabled = false;
		about.enabled = false;
		exit.enabled = false;
		mainCamera.GetComponent<Animator> ().Play ("zoomInfo");

	}

	public void backPress(){

		StartCoroutine (backPressed ());
		aboutMenu.enabled = false;
		back.enabled = false;
		mainCamera.GetComponent<Animator> ().Play ("zoomOutInfo");
	}

	public void beginlevel(){
		StartCoroutine (beginPressed ());
		mainCamera.GetComponent<Animator> ().Play ("zoomBegin");
		//lightOut.GetComponent<Animator> ().Play ("lightBeginOut");
		fader.GetComponent<Animator> ().Play ("Fade");
		background.GetComponent<Animator> ().Play ("drumsFade");
		startMenu.enabled = false;
		begin.enabled = false;
		about.enabled = false;
		exit.enabled = false;
	}

	public void exitGame(){
		Application.Quit ();

	}
		
	IEnumerator aboutPressed(){
		yield return new WaitForSeconds (1);
		aboutMenu.enabled = true;
		back.enabled = true;
		}

	IEnumerator backPressed(){
		yield return new WaitForSeconds (1);
		startMenu.enabled = true;
		begin.enabled = true;
		about.enabled = true;
		exit.enabled = true;
	}

	IEnumerator beginPressed(){
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene (1);
	}
}
