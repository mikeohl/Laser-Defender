/* ScoreDisplay displays score and then resets the score.
 * For use at end of game. 
 */

using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text myText = GetComponent<Text>();
		myText.text = ScoreKeeper.score.ToString();
		ScoreKeeper.Reset ();
	}
}
