using UnityEngine;
using UnityEngine.SceneManagement;
using GameSerialization;

public class LevelPassing : MonoBehaviour {

    public string nextScene;
    public Transform levelBackPoint;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            PersistenceManager.instance.loadingPanel.SetActive(true);
            collider.transform.position = levelBackPoint.position;
            PersistenceManager.instance.Save();

            SceneManager.LoadScene(nextScene);
        }
    }
}
