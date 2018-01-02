using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public string startLevel;

	public void StartNewGame() {
        SceneManager.LoadScene(startLevel);
    }
}
