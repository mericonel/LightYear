using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ACR.TimeOfDayFree;

public class Example_UI_Manager : MonoBehaviour 
{


	public TimeOfDayManager TOD_Manager;
	public GameObject UIObject;

	public Slider worldLongitude;
	public Toggle playTime;
	public Slider dayInSeconds;
	public Slider timeLine;
	public Text   time;

	private bool enableUI = true;


	void Start()
	{

		worldLongitude.value = TOD_Manager.WorldLongitude;
		playTime.isOn        =  TOD_Manager.playTime;
		timeLine.value       =  TOD_Manager.currentTime;
		dayInSeconds.value   =  TOD_Manager.dayInSeconds;
	}

	void Update()
	{

		if (Input.GetKeyDown (KeyCode.G))
			enableUI = !enableUI;

		UIObject.SetActive (enableUI);

		TOD_Manager.WorldLongitude = worldLongitude.value;
		TOD_Manager.playTime = playTime.isOn;

		time.text =  TOD_Manager.TimeString;

		if (! TOD_Manager.playTime) 
		{
			TOD_Manager.currentTime = timeLine.value;
		} 
		else 
		{

			TOD_Manager.dayInSeconds = dayInSeconds.value;
			timeLine.value           = TOD_Manager.currentTime;
		}
	}

}
