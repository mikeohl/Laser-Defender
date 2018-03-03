/* PlayerController controls the player ship during the game.
 * It updates player position through player input on the 
 * keyboard. Player can move ship left or right and fire 
 * projectile at enemies. Player loses health when hit and ends
 * game when health is gone.
 */

using UnityEngine;

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

    // Update is called once per frame
    // Handle player (keyboard) input for movement and laser fire 
    void Update () {
        // Fire at constant rate with key hold 
        if (Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire", 0.000001f, firingRate); 
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke();
		}
	
        // Player movement
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;;
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
	
		// Restrict the player to the game space
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
	
    // Player loses health if hit by enemy projectile
	void OnTriggerEnter2D (Collider2D collider) {
		Projectile laser = collider.gameObject.GetComponent<Projectile>();
		if (laser) {
			Debug.Log ("Player Hit");
			health -= laser.GetDamage ();
			laser.Hit();
			if (health <= 0) {
				GameOver();
			}
		}
	}

    // Fire laser at enemy
    public void Fire() {
        GameObject beam = Instantiate(projectile, transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    // End game and load end screen
    public void GameOver () {
		Destroy (gameObject);
		AudioSource.PlayClipAtPoint(explosion, transform.position);
		LevelManager manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		manager.LoadLevel("End Screen");
	}
}
