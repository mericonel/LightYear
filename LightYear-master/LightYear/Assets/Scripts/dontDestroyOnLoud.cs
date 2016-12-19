using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class dontDestroyOnLoud : MonoBehaviour {

	bool canContinue = false;
	bool canContinue2 = false;
	bool totemInstruction = false;

	public AudioSource interfaceSound;

	public GameObject mainObj;
	public GameObject scoreboard;
	public GameObject actualCanvas;

	public GameObject pauseStuffcanvas;
	public GameObject controller1;
	public GameObject arrow1;
	public GameObject arrowKeys;
	public GameObject description1;
	public GameObject aKeyboard;
	public GameObject dKeyboard;
	public GameObject arrow2;
	public GameObject controller2;
	public GameObject description2;

	private AudioSource musicP;

	public GameObject musicPlayer;

	public GameObject pauseStuff2canvas;
	public GameObject description3;
	public GameObject xButton;
	public GameObject bButton;
	public GameObject description4;
	public GameObject description5;

	Movement timeScaleVar;
	ScoreTracker scoreReach;

	void Start(){
		timeScaleVar = mainObj.GetComponent<Movement> ();
		scoreReach = scoreboard.GetComponent<ScoreTracker> ();
		StartCoroutine (pauseStuff ());

	}
	void Awake() {

		//When the scene loads it checks if there is an object called "MUSIC".
		musicPlayer = GameObject.Find("gameMusic");
		if(musicPlayer==null)
		{
			//If this object does not exist then it does the following:
			//1. Sets the object this script is attached to as the music player
			musicPlayer = this.gameObject;
			//2. Renames THIS object to "MUSIC" for next time
			musicPlayer.name = "gameMusic";
			//3. Tells THIS object not to die when changing scenes.
			DontDestroyOnLoad(musicPlayer);
			musicP = musicPlayer.GetComponent<AudioSource> ();
		}else{
			if(this.gameObject.name!="gameMusic"){
				//If there WAS an object in the scene called "MUSIC" (because we have come back to
				//the scene where the music was started) then it just tells this object to 
				//destroy itself if this is not the original
				Destroy(this.gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if ((SceneManager.GetActiveScene().name == "0") && musicP.isPlaying) {
			// stop the music
			musicP.Stop();
		} else if (musicP.isPlaying == false) {
			musicP.Play();
		}

		if (scoreReach.isEnoughPoints2 == true && totemInstruction == false) {
			StartCoroutine (pauseStuff2 ());
			totemInstruction = true;
		}

		if (canContinue == true) {
			if (Input.GetButton ("AButton")) {
				Destroy (pauseStuffcanvas.gameObject);
				GetComponent<Animator> ().Play ("bigVolume");
				Time.timeScale = 1;
				canContinue = false;
			}
		}

		if (canContinue2 == true) {
			if (Input.GetButton ("AButton")) {
				Destroy (pauseStuff2canvas.gameObject);
				actualCanvas.SetActive (true);
				GetComponent<Animator> ().Play ("bigVolume");
				Time.timeScale = timeScaleVar.timeScaler;
				canContinue2 = false;
			}
		}
				
	}


	IEnumerator pauseStuff (){
		yield return new WaitForSeconds (5);
		GetComponent<Animator> ().Play ("littleVolume");
		yield return new WaitForSeconds (1);
		interfaceSound.Play ();
		Time.timeScale = 0;
		controller1.SetActive (true);
		arrow1.SetActive (true);
		arrowKeys.SetActive (true);
		description1.SetActive (true);
		aKeyboard.SetActive (true);
		dKeyboard.SetActive (true);
		arrow2.SetActive (true);
		controller2.SetActive (true);
		description2.SetActive (true);
		canContinue = true;
	}

	IEnumerator pauseStuff2 (){
		GetComponent<Animator> ().Play ("littleVolume");
		yield return new WaitForSeconds (1);
		actualCanvas.SetActive (false);
		interfaceSound.Play ();
		Time.timeScale = 0;
		description3.SetActive (true);
		xButton.SetActive (true);
		bButton.SetActive (true);
		description4.SetActive (true);
		description5.SetActive (true);
		canContinue2 = true;
	}
}
