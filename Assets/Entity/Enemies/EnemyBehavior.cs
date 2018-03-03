/* EnemyBehavior manages enemy health, enemy projectile attack, 
 * and enemy destruction. 
 */

using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	public GameObject projectile;
	public float health = 100.0f;
	public float projectileSpeed;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;
	
	public AudioClip fireSound;
	public AudioClip explosionSound;
	
	private ScoreKeeper scoreKeeper;

	// Use this for initialization
	void Start () {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}
	
	// Update is called once per frame
	void Update () {
		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability) {
			Fire ();
		}
	}
	
    // Fire enemy projectile
	void Fire() {
		GameObject laser = Instantiate (projectile, transform.position + new Vector3(0, -0.6f, 0),Quaternion.Euler (new Vector3(0,0,180))) as GameObject;		
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
    // Reduce health on collision with player laser projectile
    // Destroy enemy object if health is gone
	void OnTriggerEnter2D (Collider2D collider) {
		Projectile laser = collider.gameObject.GetComponent<Projectile>();
		if (laser) {
			health -= laser.GetDamage ();
			laser.Hit();
			if (health <= 0) {
				Explode();
			}
		}
	}
	
    // Play explosion audio and destroy this game object
	void Explode () {
		AudioSource.PlayClipAtPoint(explosionSound, transform.position);
		Destroy (gameObject);
		scoreKeeper.Score(scoreValue);
	}
}
