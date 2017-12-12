using UnityEngine;
using System.Collections;

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
	
	void Fire() {
		GameObject laser = Instantiate (projectile, transform.position + new Vector3(0, -0.6f, 0),Quaternion.Euler (new Vector3(0,0,180))) as GameObject;		
		laser.rigidbody2D.velocity = new Vector3(0, -projectileSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		Projectile laser = collider.gameObject.GetComponent<Projectile>();
		if (laser) {
			health -= laser.GetDamage ();
			laser.Hit();
			if (health <= 0) {
				Explode();
			}
			// Debug.Log ("Hit by a projectile");
		}
	}
	
	void Explode () {
		AudioSource.PlayClipAtPoint(explosionSound, transform.position);
		Destroy (gameObject);
		scoreKeeper.Score(scoreValue);
	}
}
