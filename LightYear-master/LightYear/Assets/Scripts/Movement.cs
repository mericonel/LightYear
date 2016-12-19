using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using AcidTrip;

public class Movement : MonoBehaviour {

	public int speed = 5;
	private float balance = 0f;
	public float timeScaler;
	private float balanceValue;
	private float balanceChangeTime = 3.0f;

	public Transform mainObjTrans;

	public GameObject character;
	public GameObject canvas;
	public GameObject date;
	public GameObject mainCamera;
	public GameObject musicPlayer;
	public GameObject restarter;
	public GameObject rotateMan;
	public GameObject wheel;
	public GameObject mainObj;
	public GameObject spotlight;
	public GameObject fxFire;
	public GameObject destroyer;
	public GameObject scoreboard;

	public GameObject spawnerObj1;
	public GameObject spawnerObj2;
	public GameObject steam;

	public GameObject faster;
	public GameObject rocks;
	public GameObject evenFaster;
	public GameObject earthquakeCanvas;
	public GameObject thunderstormCanvas;

	public GameObject totem1;
	public GameObject totem2;
	public GameObject redParticles;
	public GameObject blueParticles;
	public GameObject transformation;
	public GameObject chariot;
	public GameObject neutralize;
	bool isChariot = false;
	bool isNeutralize = false;

	public GameObject fireworks1;
	public GameObject fireworks2;

	public Slider slider;
	public GameObject sliderObj;
	private float Handler;

	public bool isKeysDisabled;

	bool isEvenFast = false;
	bool isEarthquake = false;
	bool isThunderstorm = false;
	bool timeScalerAllowance = true;

	public AudioSource stomp1;
	public AudioSource frost;
	public AudioSource diffUp;
	public AudioSource stampede1;
	public AudioSource thunderstormSound;
	public AudioSource evilLaugh;
	public AudioSource redTotem;
	public AudioSource blueTotem;
	public AudioSource totemDeath;

	// Use this for initialization
	void Start () {

		timeScaler = 1.0f;

		if (isChariot == false) {
			InvokeRepeating ("HandleMoves", 6.0f, balanceChangeTime);
		}
		InvokeRepeating ("difficulty", 60.0f, 30.0f);
		StartCoroutine (activater ());
		StartCoroutine (diffFast ());

	}
	
	// Update is called once per frame
	void Update () {

		//activating the special totem abilities
		if (timeScalerAllowance == false && isEarthquake == false && isKeysDisabled == false) {
			if (scoreboard.GetComponent<ScoreTracker> ().isEnoughPoints2 == true && isChariot == false && isNeutralize == false) {
				if (Input.GetButton ("BButton")) {
					scoreboard.GetComponent<ScoreTracker> ().addScore (-100);
					isChariot = true;
					redTotem.Play ();
					character.GetComponent<BoxCollider> ().enabled = false;
					redParticles.SetActive (true);
					StartCoroutine (redAbilityParticles ());
				}
			}
		}
		if (timeScalerAllowance == false && isEarthquake == false && isKeysDisabled == false) {
		if (scoreboard.GetComponent<ScoreTracker> ().isEnoughPoints1 == true && isChariot == false && isNeutralize == false) {
				if (Input.GetButton ("XButton")) {
					Debug.Log ("x pressed");
					scoreboard.GetComponent<ScoreTracker> ().addScore (-250);
					isNeutralize = true;
					blueParticles.SetActive (true);
					blueTotem.Play ();
					StartCoroutine (blueAbilityParticles ());
				}
			}
		}

		if (timeScalerAllowance == false) {
			if (isEarthquake == true) {
				//Time.timeScale = 1;
				balanceChangeTime = 1.5f;
			} else if (isEarthquake == false){
				//Time.timeScale = timeScaler;
				balanceChangeTime = 3.0f;
			}
		}

		//MOVEMENT
	transform.position += Vector3.forward * speed * Time.deltaTime;

				if (isKeysDisabled == false) {
			float movementAxis = Input.GetAxis ("Horizontal") * Time.deltaTime * speed;
					transform.Translate (movementAxis, 0, 0);
				}

		if (isThunderstorm == true) {
			speed = 8;
		} else if (isChariot == true) {
			speed = 12;
			Handler = 0;
			sliderObj.SetActive (false);
		} else {
			speed = 5;
			sliderObj.SetActive (true);
		}

		Vector3 clampPosition = transform.position;
		clampPosition.x = Mathf.Clamp (transform.position.x, -9f, 9f);
		transform.position = clampPosition;

		//ROTATION OUCHIE

		rotateMan.transform.eulerAngles = new Vector3 (0, 0, balance * 1.3f);
		slider.value += Handler * Time.deltaTime;

		if (Input.GetKey ("a")) {
			if (isKeysDisabled == false) {
				slider.value += 8 * Time.deltaTime;
			} 
		}

		if (Input.GetKey ("d")) {
			if (isKeysDisabled == false) {
				slider.value += -8 * Time.deltaTime;
			}
		}
			
		if (Input.GetButton ("LBumper")){
			if (isKeysDisabled == false) {
				slider.value += 8 * Time.deltaTime;
			}
		}

		if (Input.GetButton ("RBumper")){
			if (isKeysDisabled == false) {
				slider.value += -8 * Time.deltaTime;
			}
		}

		//falls to the left
		if (slider.value == 15) {
			character.GetComponent<Animator> ().Play ("FallLeft");
			StartCoroutine (reset2());
			StartCoroutine (fireworks ());

		}

		//falls to the right
		if (slider.value == -15) {
			character.GetComponent<Animator> ().Play ("FallRight");
			StartCoroutine (reset2()); 
			StartCoroutine (fireworks ());

		}

		//just so we can escape
		if (speed == 0){

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

	public void AdjustBalance(float balanceValue){
		balance = balanceValue;
	}

	private void HandleMoves(){
			Handler = Random.Range (0, 2) == 0 ? -4 : 4;
	}

	private void difficulty(){
		float diffValue = Random.Range (0, 4);

		if (isEvenFast == false) {
			if (diffValue <= 4 && diffValue > 3) {
				StartCoroutine (diffEvenFast ());
				isEvenFast = true;
			}
		} else if (isEvenFast == true) {
			if (diffValue <= 4 && diffValue > 3) {
				diffValue = Random.Range (0, 3);
				isEvenFast = false;
			}
		}
			
		if (diffValue <= 3 && diffValue > 2) {
			if (spawnerObj2.activeInHierarchy == false) {
				StartCoroutine (diffRocks ());
			} else if (spawnerObj2.activeInHierarchy == true) {
				diffValue = Random.Range (0, 4);
			}
		}

		if (diffValue <= 2 && diffValue > 1) {
			StartCoroutine (earthquake ());
		}

		if (diffValue <= 1 && diffValue > 0) {
			StartCoroutine (thunderstorm ());
		}
	}

	IEnumerator reset2(){
		//frost.Play ();
		//stomp1.Play ();
		Destroy (destroyer.gameObject);
		CancelInvoke ("HandleMoves");
		Handler = 0;
		speed = 0;
		character.GetComponent<BoxCollider> ().enabled = false;
		mainCamera.GetComponent<Animator> ().Play ("Frost");
		Destroy (wheel.GetComponent<Animator> ());
		isKeysDisabled = true;
		canvas.SetActive (false);
		yield return new WaitForSeconds (3);
		restarter.SetActive (true);
}

	IEnumerator activater(){
		yield return new WaitForSeconds (2.5f);
		date.SetActive (true);
		yield return new WaitForSeconds (2.5f);
		Destroy (date.gameObject);
		canvas.SetActive (true);
		yield return new WaitForSeconds (2);
		timeScalerAllowance = false;
	}

	IEnumerator diffFast(){
		yield return new WaitForSeconds (30);
		if (isKeysDisabled == false) {
			timeScaler += 0.2f;
			faster.SetActive (true);
			diffUp.Play ();
		}
		yield return new WaitForSeconds (0.5f);
			faster.SetActive (false);
		yield return new WaitForSeconds (0.5f);
		if (isKeysDisabled == false) {
			faster.SetActive (true);
			diffUp.Play ();
		}
		yield return new WaitForSeconds (0.5f);
			faster.SetActive (false);
		yield return new WaitForSeconds (0.5f);
		if (isKeysDisabled == false) {
			faster.SetActive (true);
			diffUp.Play ();
		}
		yield return new WaitForSeconds (1);
			faster.SetActive (false);
		evilLaugh.Play ();
	}

	IEnumerator diffRocks(){
		if (isKeysDisabled == false) {
			
			if (spawnerObj1.activeInHierarchy == true) {
				spawnerObj2.SetActive (true);
			} else if (spawnerObj1.activeInHierarchy == false) {
				spawnerObj1.SetActive (true);
			}

			rocks.SetActive (true);
			diffUp.Play ();
		}
		yield return new WaitForSeconds (0.5f);
			rocks.SetActive (false);
		yield return new WaitForSeconds (0.5f);
		if (isKeysDisabled == false) {
			rocks.SetActive (true);
			diffUp.Play ();
		}
		yield return new WaitForSeconds (0.5f);
			rocks.SetActive (false);
		yield return new WaitForSeconds (0.5f);
		if (isKeysDisabled == false) {
			rocks.SetActive (true);
			diffUp.Play ();
		}
		yield return new WaitForSeconds (1);
			rocks.SetActive (false);
		evilLaugh.Play ();

	}

	IEnumerator diffEvenFast(){
		if (isKeysDisabled == false) {
			timeScaler += 0.1f;
			evenFaster.SetActive (true);
			diffUp.Play ();
		}
		yield return new WaitForSeconds (0.5f);
			evenFaster.SetActive (false);
		yield return new WaitForSeconds (0.5f);
		if (isKeysDisabled == false) {
			evenFaster.SetActive (true);
			diffUp.Play ();
		}
		yield return new WaitForSeconds (0.5f);
			evenFaster.SetActive (false);
		yield return new WaitForSeconds (0.5f);
		if (isKeysDisabled == false) {
			evenFaster.SetActive (true);
			diffUp.Play ();
		}
		yield return new WaitForSeconds (1);
			evenFaster.SetActive (false);
		evilLaugh.Play ();

	}

	IEnumerator earthquake(){
		if (isKeysDisabled == false) {
			isEarthquake = true;
			mainCamera.GetComponent<Animator> ().Play ("CameraShake");
			stampede1.Play ();
			earthquakeCanvas.SetActive (true);
			diffUp.Play ();
		}
		yield return new WaitForSeconds (0.5f);
			earthquakeCanvas.SetActive (false);
		yield return new WaitForSeconds (0.5f);
		if (isKeysDisabled == false) {
			earthquakeCanvas.SetActive (true);
			diffUp.Play ();
		}
		yield return new WaitForSeconds (0.5f);
			earthquakeCanvas.SetActive (false);
		yield return new WaitForSeconds (0.5f);
		if (isKeysDisabled == false) {
			earthquakeCanvas.SetActive (true);
			diffUp.Play ();
		}
		yield return new WaitForSeconds (1);
			earthquakeCanvas.SetActive (false);
		evilLaugh.Play ();
		yield return new WaitForSeconds (7);
		isEarthquake = false;

	}

	IEnumerator thunderstorm(){
		if (isKeysDisabled == false) {
			isThunderstorm = true;
			steam.SetActive (true);
			thunderstormSound.Play ();
			thunderstormCanvas.SetActive (true);
			diffUp.Play ();
		}
		yield return new WaitForSeconds (0.5f);
			thunderstormCanvas.SetActive (false);
		yield return new WaitForSeconds (0.5f);
		if (isKeysDisabled == false) {
			thunderstormCanvas.SetActive (true);
			diffUp.Play ();
		}
		yield return new WaitForSeconds (0.5f);
			thunderstormCanvas.SetActive (false);
		yield return new WaitForSeconds (0.5f);
		if (isKeysDisabled == false) {
			thunderstormCanvas.SetActive (true);
			diffUp.Play ();
		}
		yield return new WaitForSeconds (1);
		evilLaugh.Play ();
			thunderstormCanvas.SetActive (false);
		yield return new WaitForSeconds (5);
		steam.GetComponent<Animator> ().Play ("steamFades");
		yield return new WaitForSeconds (12);
		steam.SetActive (false);
		isThunderstorm = false;

	}

	IEnumerator redAbilityParticles(){
		transformation.SetActive (true);
		rotateMan.SetActive (false);
		yield return new WaitForSeconds (2);
		mainCamera.GetComponent<AcidTrip.AcidTrip>().enabled = true;
		chariot.SetActive (true);
		transformation.SetActive (false);
		yield return new WaitForSeconds (7);
		transformation.SetActive (true);
		yield return new WaitForSeconds (1.5f);
		isChariot = false;
		mainCamera.GetComponent<AcidTrip.AcidTrip>().enabled = false;
		redParticles.SetActive (false);
		chariot.SetActive (false);
		rotateMan.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		transformation.SetActive (false);
		yield return new WaitForSeconds (2.5f);
		character.GetComponent<BoxCollider> ().enabled = true;

	}

	IEnumerator blueAbilityParticles(){
		yield return new WaitForSeconds (2);
		neutralize.SetActive (true);
		yield return new WaitForSeconds (4);
		timeScaler = 1;
		spawnerObj1.SetActive (false);
		spawnerObj2.SetActive (false);
		neutralize.SetActive (false);
		blueParticles.SetActive (false);
		isNeutralize = false;


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
