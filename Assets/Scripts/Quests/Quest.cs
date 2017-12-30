using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Inventory/Quests/Basic")]
public class Quest : ScriptableObject {

    public string title;
    public string description;
    public QuestReward reward;
    [HideInInspector]
    public Npc npc;
    [HideInInspector]
    public QuestStatus status = QuestStatus.NONE;

    public virtual bool IsFinnished() {
        return true;
    }

    public virtual void GetReward() {

    }
}

public enum QuestStatus { NONE, IN_PROGRESS, FINISHED }
