using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject projectile;
	public float health			 = 100.0f;
	public float speed 			 = 8.0f;
	public float padding 		 = 0.5f;
	public float projectileSpeed = 1.0f;
	public float firingRate		 = 0.2f;
	
	public AudioClip fireSound;
	public AudioClip explosion;
	
	private float xmin;
	private float xmax;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		xmax = rightmost.x - padding;
		xmin = leftmost.x + padding;
	}	
	
	void Fire() {
		GameObject beam = Instantiate (projectile, transform.position + new Vector3 (0, 0.6f, 0), Quaternion.identity) as GameObject;		
		beam.rigidbody2D.velocity = new Vector3(0, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			// Debug.Log ("space bar pressed");
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			// Debug.Log ("space bar released");
			CancelInvoke();
		}
	
		if (Input.GetKey(KeyCode.LeftArrow)) {
			// Debug.Log ("left arrow pressed");
			transform.position += Vector3.left * speed * Time.deltaTime;;
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			// Debug.Log ("right arrow pressed");
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
	
		// Restrict the player to the game space
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		Projectile laser = collider.gameObject.GetComponent<Projectile>();
		if (laser) {
			Debug.Log ("Player Hit");
			health -= laser.GetDamage ();
			laser.Hit();
			if (health <= 0) {
				GameOver();
			}
			// Debug.Log ("Hit by a projectile");
		}
	}
	
	void GameOver () {
		Destroy (gameObject);
		// AudioSource.PlayClipAtPoint(explosion, transform.position);
		LevelManager manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		manager.LoadLevel("End Screen");
	}
}
