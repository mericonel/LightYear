using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Transition1Script : MonoBehaviour {

	public GameObject cameraT;
	public GameObject fader;
	public GameObject stoneWheel;
	public GameObject cart;
	public GameObject light1;
	public GameObject torch;
	public GameObject rain;
	public GameObject steam;
	public GameObject lastActive; //girl1, barrels, snow, tent, horses
	public GameObject mountain;
	public GameObject terrain;
	public GameObject familyObject1;
	public GameObject horseCharacters;
	public GameObject tent;
	public GameObject snow;

	// Use this for initialization
	void Start () {

		fader.GetComponent<Animator> ().Play ("FadeIn1");
		StartCoroutine (beginning ());
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator beginning (){
		yield return new WaitForSeconds (3);
		cameraT.GetComponent<Animator> ().Play ("Transition1");
		yield return new WaitForSeconds (14.5f);
		cameraT.GetComponent<Animator> ().Play ("Transition 1-0");
		Destroy (stoneWheel.gameObject);
		Destroy (torch.gameObject);
		Destroy (familyObject1.gameObject);
		horseCharacters.SetActive (true);
		light1.SetActive (true);
		cart.SetActive (true);
		rain.SetActive (true);
		steam.SetActive (true);
		yield return new WaitForSeconds (8.5f);
		cart.GetComponent<Animator> ().Play ("cartMoves");
		Destroy (tent.gameObject);
		Destroy (horseCharacters.gameObject);
		Destroy (rain.gameObject);
		Destroy (steam.gameObject);
		Destroy (terrain.gameObject);
		lastActive.SetActive (true);
		snow.SetActive (true);
		mountain.SetActive (true);
		GetComponent<Animator> ().Play ("cartMovesSlowly");
		yield return new WaitForSeconds (6.5f);
		fader.GetComponent<Animator> ().Play ("Fade");
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene (3);

	}
}
