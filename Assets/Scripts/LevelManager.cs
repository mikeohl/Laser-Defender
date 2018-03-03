/* LevelManager is responsible for loading the 
 * next scene and quiting the application.
 * Uses Unity Scene Management for loading scenes. 
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel(string name) {
        // Debug.Log("Level load requested for: " + name);
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitRequest() {
        // Debug.Log("Player wants to quit");
        Application.Quit();
    }
}
