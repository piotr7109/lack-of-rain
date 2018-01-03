using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetsManager : MonoBehaviour {

    #region Singleton

    public static AssetsManager instance;

    void Awake() {
        instance = this;
    }

    #endregion

    public List<ItemAsset> itemAssets;
    public List<QuestAsset> questAssets;

    public static Item GetItem(string name) {
        ItemAsset result = instance.itemAssets.Find(item => item.name == name);
        return result != null ? result.item : null;
    }

    public static Quest GetQuest(string title) {
        QuestAsset result = instance.questAssets.Find(item => item.name == title);
        return result != null ? result.quest : null;
    }
}

[System.Serializable]
public class ItemAsset : Asset {
    [SerializeField]
    public Item item;
}

[System.Serializable]
public class QuestAsset :Asset {
    [SerializeField]
    public Quest quest;
}

public class Asset {
    [SerializeField]
    public string name;
}
