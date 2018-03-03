/* MusicPlayer initiates music track for each specific level
 * through a persistent music player instance.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer instance = null;
	
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;
	
	private AudioSource music;

    // Play music when scene is loaded through Unity SceneManagement
    void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }
    void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        int level = scene.buildIndex;

        // Debug.Log("MusicPlayer: loaded level " + level);
        if (music) {
            music.Stop();
            if (level == 0) {
                music.clip = startClip;
            } else if (level == 1) {
                music.clip = gameClip;
            } else if (level == 2) {
                music.clip = endClip;
            }

            music.loop = true;
            music.Play();
        }
    }

    // Use this for initialization
    void Start () {
        // Create a persistent music player and destroy new music player
        // when we return to the start screen
        // Debug.Log("Music player Start " + GetInstanceID());
        if (instance != null && instance != this) { 
			Destroy (gameObject);
			Debug.Log("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.loop = true;
			music.Play();
		}
	}
}
