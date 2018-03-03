/* Position uses OnDrawGizmos to visualize position of
 * enemy spawn points in editor
 */

using UnityEngine;

// Visualize enemy spawn points in editor
public class Position : MonoBehaviour {

	void OnDrawGizmos() {
		Gizmos.DrawWireSphere(transform.position, 1);
	}
}
