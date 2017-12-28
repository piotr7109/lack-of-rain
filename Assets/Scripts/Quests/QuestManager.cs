using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour {

    #region Singleton

    public static QuestManager instance;

    void Awake() {
        instance = this;
    }

    #endregion

    public List<Quest> quests = new List<Quest>();

    public void AddQuest(Quest quest) {
        quest.status = QuestStatus.IN_PROGRESS;
        quests.Add(quest);
    }

    public void FinishQuest(Quest quest) {
        quest.GetReward();
        quest.status = QuestStatus.FINISHED;
    }

}
