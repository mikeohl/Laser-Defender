using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public static int score = 0;
	
	private Text myText;

	// Use this for initialization
	void Start () {
		myText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Score (int points) {
		Debug.Log ("Scored Points"); 
		score += points;
		myText.text = "Score: " + score.ToString ();
	}
	
	public static void Reset () {
		score = 0;
	}
}
