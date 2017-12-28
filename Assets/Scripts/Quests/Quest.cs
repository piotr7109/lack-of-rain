using UnityEngine;

public class Quest : MonoBehaviour {

    public string title;
    public string description;
    public QuestReward reward;
    [HideInInspector]
    public Npc npc;
    [HideInInspector]
    public QuestStatus status = QuestStatus.NONE;
    protected LevelsManager levelsManager;

    protected virtual void Start() {
        levelsManager = LevelsManager.instance;
    }

    public virtual bool IsFinnished() {
        return true;
    }

    public virtual void GetReward() {

    }
}

public enum QuestStatus { NONE, IN_PROGRESS, FINISHED }
