using UnityEngine;

namespace GameSerialization {

    [System.Serializable]
    public class Position {
        float x, y, z;

        public Position(Vector3 pos) {
            x = pos.x;
            y = pos.y;
            z = pos.z;
        }

        public Vector3 GetVector() {
            return new Vector3(x, y, z);
        }
    }
}