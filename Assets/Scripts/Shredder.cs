/* Shredder destroys game objects that trigger collider
 * object. Keeps laser projectiles from proliferating
 * continuously.
 */

using UnityEngine;

public class Shredder : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		Destroy(collider.gameObject);
	}
}
