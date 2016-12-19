using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreShow : MonoBehaviour {

	Text scoreText;
	ScoreTracker getScore;
	public GameObject scoreboard;

	// Use this for initialization
	void Start () {

		getScore = scoreboard.GetComponent<ScoreTracker> (); 

		scoreText = GetComponent<Text> ();
		scoreText.text = " " + getScore.score;
	
	}
	
	// Update is called once per frame
	void Update () {

		scoreText.text = "Your Score: " + getScore.score;
	
	}
}
