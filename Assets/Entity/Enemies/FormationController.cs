/* FormationController handles enemy formation movement and spawning.
 */

using UnityEngine;

public class FormationController : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width 		= 10.0f;
	public float height 	= 5.0f;
	public float speed 		= 2.0f;
	public float spawnDelay = 0.5f;
	
	private float xmin;
	private float xmax;
	private bool direction 	= true;
	
	// Use this for initialization
	void Start () {
        // Initialize viewspace for formation movement control
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceToCamera));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distanceToCamera));
		xmin = leftEdge.x + width/2;
		xmax = rightEdge.x - width/2;
		
		SpawnUntilFull ();
	}

    // Update is called once per frame
    void Update () {
        // Move formation left and right
		if (direction) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (transform.position.x >= xmax) {
			direction = false;
		} else if (transform.position.x <= xmin) {
			direction = true;
		}
		
        // Respawn formation if all enemies defeated
		if (AllMembersDead()) {
			Debug.Log ("Empty Formation");
			SpawnUntilFull ();
		}
	}
	
    // Fill formation with enemies until full
	void SpawnUntilFull() {
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition ()) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

    // Find the first available free position in formation
    Transform NextFreePosition () {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;
	}
	
    // Check if there are any enemies in the formation
	bool AllMembersDead () {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}

    // Display the wireframe for game development
    public void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }
}
