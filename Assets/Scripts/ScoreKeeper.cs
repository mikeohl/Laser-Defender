/* ScoreKeeper maintains the score of the game in a static
 * variable. Can reset on a new game.
 */

using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public static int score = 0;
	
	private Text myText;

	// Use this for initialization
	void Start () {
		myText = GetComponent<Text>();
	}
	
    // Add points to score and update in-game text
	public void Score (int points) {
		score += points;
		myText.text = "Score: " + score.ToString ();
	}
	
	public static void Reset () {
		score = 0;
	}
}
