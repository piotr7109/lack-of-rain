using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameSerialization;

public class StartMenu : MonoBehaviour {

    public string startLevel;

    private Transform loadGame;
    private const string SAVE_FILE = "/saves/SaveData.dat";

    void Start() {
        loadGame = transform.Find("LoadGame");
        Debug.Log(File.Exists(Application.dataPath + SAVE_FILE));
        loadGame.gameObject.SetActive(File.Exists(Application.dataPath + SAVE_FILE));
    }

    public void StartNewGame() {
        if (File.Exists(Application.dataPath + SAVE_FILE)) {
            File.Delete(Application.dataPath + SAVE_FILE);
        }

        SceneManager.LoadScene(startLevel);

    }

    public void LoadGame() {
        SceneManager.LoadScene(GetLoadGameScene());
    }

    string GetLoadGameScene() {
        GameData gameData = new GameData();
        gameData.LoadSavedGame();

        return gameData.currentLevel;
    }
}
