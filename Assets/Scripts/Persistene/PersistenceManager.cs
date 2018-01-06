using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSerialization {
    public class PersistenceManager : MonoBehaviour {

        #region Singleton
        public static PersistenceManager instance;

        void Awake() {
            instance = this;
        }

        #endregion

        public GameObject loadingPanel;
        public Transform npcPrefab;
        public Transform itemPrefab;
        public Transform startPoint;

        void Update() {
            if (Input.GetKeyDown(KeyCode.O)) {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.P)) {
                Load();
            }
        }

        public void Save() {
            new GameData().SaveGame();
        }

        public void Load() {
            GameData gameData = new GameData();
            gameData.LoadSavedGame();

            SceneManager.LoadScene(gameData.currentLevel);
        }

        void DestroySceneElements() {
            Array.ForEach(new string[] { "Enemy", "Item" }, key => Array.ForEach(GameObject.FindGameObjectsWithTag(key), go => Destroy(go)));
        }

        void Start() {
            loadingPanel.SetActive(true);
            StartCoroutine(LoadSceneElements());
        }

        IEnumerator LoadSceneElements() {
            yield return new WaitForSeconds(.1f);

            GameData gameData = new GameData();
            gameData.LoadSavedGame();
            string activeScene = SceneManager.GetActiveScene().name;

            if (gameData.player != null) {
                gameData.player.CreateInstance(PlayerManager.instance.player.transform, GameObject.Find("GameManager").transform);
            }

            if (gameData.scenes.ContainsKey(activeScene)) {
                SceneData data = gameData.scenes[activeScene];

                DestroySceneElements();
                data.npcs.ForEach(npc => npc.CreateInstance(npcPrefab));
                data.items.ForEach(item => item.CreateInstance(itemPrefab));
            } else {
                PlayerManager.instance.player.transform.position = startPoint.position;
                gameData.SaveGame();
            }

            loadingPanel.SetActive(false);
        }
    }

    [Serializable]
    public class GameData {
        public string currentLevel;
        public Dictionary<string, SceneData> scenes;
        public SerializedPlayer player;

        private const string SAVE_FILE = "/saves/SaveData.dat";

        public void SaveGame() {
            if (File.Exists(Application.dataPath + SAVE_FILE)) {
                LoadSavedGame();
            } else {
                File.Create(Application.dataPath + SAVE_FILE).Close();
                scenes = new Dictionary<string, SceneData>();
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + SAVE_FILE, FileMode.Open);

            currentLevel = SceneManager.GetActiveScene().name;
            scenes[currentLevel] = new SceneData();
            player = PlayerSerialization.GetSerialized();

            bf.Serialize(file, this);
            file.Close();
        }

        public void LoadSavedGame() {
            if (File.Exists(Application.dataPath + SAVE_FILE)) {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.dataPath + SAVE_FILE, FileMode.Open);
                GameData gameData = (GameData)bf.Deserialize(file);

                scenes = gameData.scenes;
                currentLevel = gameData.currentLevel;
                player = gameData.player;
                file.Close();
            } else {
                scenes = new Dictionary<string, SceneData>();
                currentLevel = SceneManager.GetActiveScene().name;
            }
        }
    }

    [Serializable]
    public class SceneData {
        public List<SerializedNpc> npcs;
        public List<SerializedItem> items;

        public SceneData() {
            npcs = SerializeNpcs.GetSerialized();
            items = ItemsSerialization.GetSerialized();
        }
    }
}