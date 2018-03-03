/* Projectile tracks projectile damage and can destroy
 * itself on hit of ship 
 */

using UnityEngine;

public class Projectile : MonoBehaviour {

	public float damage;

	public float GetDamage () { return damage; }
	
    // Destroys projectile game object. Call when ship is hit
	public void Hit () { Destroy(gameObject); }
}
