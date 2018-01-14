using UnityEngine;
using System;
using System.Collections.Generic;

public class AssetsManager : MonoBehaviour {

    #region Singleton

    public static AssetsManager instance;

    void Awake() {
        instance = this;
        CloneQuests();
    }

    #endregion

    public List<Item> itemAssets;
    public List<Quest> questAssets;
    
    void CloneQuests() {
        for(int i = 0; i < questAssets.Count; i++) {
            questAssets[i] = Instantiate(questAssets[i]);
        }
    }

    public static Item GetItem(string name) {
        return instance.itemAssets.Find(item => item.name == name);
    }

    public static Quest GetQuest(string title) {
        return instance.questAssets.Find(quest => quest.title == title);
    }
}
