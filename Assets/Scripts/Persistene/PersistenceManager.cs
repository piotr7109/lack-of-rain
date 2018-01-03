using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSerialization {
    public class PersistenceManager : MonoBehaviour {
        public Transform npcPrefab;


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
            FileStream file = File.Create(Application.dataPath + "/saves/SaveData.dat");
            List<SerializedNpc> npcs = SerializeNpcs.GetSerialized();

            bf.Serialize(file, npcs);

            file.Close();
        }

        void Load() {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/saves/SaveData.dat", FileMode.Open);
            List<SerializedNpc> npcs = (List<SerializedNpc>)bf.Deserialize(file);

            npcs.ForEach(npc => npc.CreateInstance(npcPrefab));

            file.Close();
        }
    }
}
