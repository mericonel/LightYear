using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderColor : MonoBehaviour {

	public Slider slider;
	public Image Fill;
	public Color blue = new Color (144F, 255F, 250F);
	public Color red = new Color (255F, 171F, 171F);
	public GameObject handle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {



		if (slider.value < 0) {
			Fill.color = Color.Lerp (red, blue, slider.value*2+12);
			handle.transform.Rotate (Vector3.forward * 4);
		} else {
			Fill.color = Color.Lerp (blue, red, slider.value*2-12);
			handle.transform.Rotate (Vector3.back * 4);
		}
	
	}
}
