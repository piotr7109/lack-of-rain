using System;
using System.Collections.Generic;
using UnityEngine;


namespace GameSerialization {
    public class ItemsSerialization : MonoBehaviour {
        public static List<SerializedItem> GetSerialized() {
            List<GameObject> items = new List<GameObject>(GameObject.FindGameObjectsWithTag("Item"));
            List<SerializedItem> serializedItems = new List<SerializedItem>();

            items.ForEach(gameObject => serializedItems.Add(new SerializedItem(gameObject)));

            return serializedItems;
        }
    }

    [Serializable]
    public class SerializedItem {
        Position position;
        string name;

        public SerializedItem(GameObject item) {
            position = new Position(item.transform.position);
            name = item.GetComponent<ItemPickup>().item.name;
        }

        public void CreateInstance(Transform transform) {
            Item item = AssetsManager.GetItem(name);

            if (item != null) {
                GameObject gameObject = GameObject.Instantiate(transform.gameObject, transform.position, transform.rotation);

                gameObject.transform.position = position.GetVector();
                gameObject.GetComponent<ItemPickup>().item = item;
                gameObject.GetComponent<SpriteRenderer>().sprite = item.icon;
            }
        }
    }
}