using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSerialization {
    public class PersistenceManager : MonoBehaviour {
        public Transform npcPrefab;

        private const string SCENES_SAVE_FILE = "/saves/SaveData.scene.dat";
        private const string SAVE_FILE = "/saves/SaveData.dat";

        void Update() {
            if (Input.GetKeyDown(KeyCode.O)) {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.P)) {
                Load();
            }
        }

        void Save() {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.dataPath + SAVE_FILE);
            FileStream sceneFile = File.Create(Application.dataPath + SCENES_SAVE_FILE);
            GameData data = new GameData();

            bf.Serialize(file, data);
            bf.Serialize(sceneFile, SceneManager.GetActiveScene().buildIndex);

            file.Close();
            sceneFile.Close();
        }

        AsyncOperation asyncLoadLevel;

        void Load() {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream sceneFile = File.Open(Application.dataPath + SCENES_SAVE_FILE, FileMode.Open);
            int sceneBuildIndex = (int)bf.Deserialize(sceneFile);
            sceneFile.Close();

            SceneManager.LoadScene(sceneBuildIndex);
        }

        /*void OnLevelWasLoaded(int level) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream sceneFile = File.Open(Application.dataPath + SCENES_SAVE_FILE, FileMode.Open);
            int sceneBuildIndex = (int)bf.Deserialize(sceneFile);
            
            FileStream file = File.Open(Application.dataPath + SAVE_FILE, FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);

            data.npcs.ForEach(npc => npc.CreateInstance(npcPrefab));

            file.Close();

        }*/
    }

    [Serializable]
    public class GameData {
        public List<SerializedNpc> npcs;

        public GameData() {
            npcs = SerializeNpcs.GetSerialized();
        }
    }
}