using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {

	private float speed = 5;
	private float balance = 0f;
	private float balanceValue;

	public GameObject character;
	public GameObject canvas;

	public Slider slider;
	float Handler;

	public AudioSource stomp1;

	// Use this for initialization
	void Start () {
		
		InvokeRepeating ("HandleMoves", 6.0f, 3.0f);
		StartCoroutine (activater ());
	}
	
	// Update is called once per frame
	void Update () {

		float x = transform.position.x;

		//MOVEMENT
	transform.position += Vector3.forward * speed * Time.deltaTime;

		if(x >= -7.0f){
			if (Input.GetKey ("left")) {
				transform.position += Vector3.left * Time.deltaTime * 5;
			} else {
			}
		} 

		if (x <= 15.0f) {
			if (Input.GetKey ("right")) {
				transform.position += Vector3.right * Time.deltaTime * 5;
			} 
		} else {
		}
			
		//ROTATION OUCHIE

		transform.eulerAngles = new Vector3 (0, 0, balance);
		slider.value += Handler * Time.deltaTime;

		if (Input.GetKey ("a")) {
			slider.value += 13 * Time.deltaTime;
		} 

		if (Input.GetKey ("d")) {
			slider.value += -13 * Time.deltaTime;
		}

		//falls to the left
		if (slider.value >= 15) {
			speed = 0;
			character.GetComponent<Animator> ().Play ("FallLeft");
			stomp1.Play ();
			StartCoroutine (reset2());
			Handler = 0;
		}

		//falls to the right
		if (slider.value <= -15) {
			speed = 0;
			character.GetComponent<Animator> ().Play ("FallRight");
			stomp1.Play ();
			StartCoroutine (reset2());
			Handler = 0;
		}

		//just so we can escape
		if(Input.GetKey(KeyCode.Escape)){
			SceneManager.LoadScene (0);
		}
			
	}

	public void AdjustBalance(float balanceValue){
		balance = balanceValue;
	}

	void HandleMoves(){
		Handler = Random.Range (0, 2) == 0 ? -9 : 9;
	}

	IEnumerator reset2(){
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene (0);
}

	IEnumerator activater(){
		yield return new WaitForSeconds (5);
		canvas.SetActive (true);
	}

}
