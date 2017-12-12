using UnityEngine;
using System.Collections;

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
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceToCamera));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distanceToCamera));
		xmin = leftEdge.x + width/2;
		xmax = rightEdge.x - width/2;
		
		SpawnUntilFull ();
	}
	
	// Display the wireframe for GameDev
	public void OnDrawGizmos () {
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
	}
	
	void SpawnFormation () {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	// Update is called once per frame
	void Update () {
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
		
		if (AllMembersDead()) {
			Debug.Log ("Empty Formation");
			SpawnUntilFull ();
		}
	}
	
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
	
	Transform NextFreePosition () {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	bool AllMembersDead () {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}
}
