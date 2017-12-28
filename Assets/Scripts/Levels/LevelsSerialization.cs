using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelsSerialization {

    private class LevelsContainer {
        public List<Level> levels;
    }

    private static string filePath = "Assets/data/levels.json";

    public static List<Level> LoadLevelsData() {
        string json = File.ReadAllText(filePath);

        return JsonUtility.FromJson<LevelsContainer>(json).levels;
    }
}
