using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {

	public int score;
	Text scoreText;

	Movement mainSpeed;
	Death askBool;

	public GameObject mainObj;
	public GameObject man;

	public bool isEnoughPoints2 = false;
	public bool isEnoughPoints1 = false;

	// Use this for initialization
	void Start () {

		score = 0;
		InvokeRepeating ("timeScore", 2.0f, 1f);

		scoreText = GetComponent<Text> ();
		scoreText.text = " " + score;

		mainSpeed = mainObj.GetComponent<Movement> ();
		askBool = man.GetComponent<Death> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (mainSpeed.isKeysDisabled == true || askBool.deathOccured == true) {
			CancelInvoke ();
		} 

		scoreText.text = score + " " ;

		if (score >= 100) {
			isEnoughPoints2 = true;
		} 

		if (score >= 250) {
			isEnoughPoints1 = true;
		}
	
	}

	public void addScore( int pointsToAdd ) {
		score += pointsToAdd;
		scoreText.text = " " + score;
	}

	public void timeScore(){
		score += 1 * mainSpeed.speed/5;
				scoreText.text = " " + score;
	}
}
