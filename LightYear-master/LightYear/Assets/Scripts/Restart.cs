using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	private float timeLeft = 10;

	public Text tryAgain;

	public Button restart;

	public AudioSource musicPlayer;

	// Use this for initialization
	void Start () {

	}

	public void reStart (){

		SceneManager.LoadScene (1);
	}
	
	// Update is called once per frame
	void Update () {

		timeLeft -= Time.deltaTime;
		tryAgain.text = "try again? " + Mathf.Round (timeLeft);
		if (timeLeft <= 0) {
			Destroy(musicPlayer.gameObject);
			SceneManager.LoadScene (0);
			//musicPlayer.GetComponent<Animator> ().Play ("musicVolume");
			//Destroy (musicPlayer.GetComponent<dontDestroyOnLoud> ());
		}
			
		}
}
