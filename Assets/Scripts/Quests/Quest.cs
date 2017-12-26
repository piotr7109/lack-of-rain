using UnityEngine;

public class Quest : MonoBehaviour {

    public string title;
    public string description;
    [HideInInspector]
    public Npc npc;
    [HideInInspector]
    public QuestStatus status = QuestStatus.NONE;

    public virtual bool IsFinnished() {
        return true;
    }
}

public enum QuestStatus { NONE, IN_PROGRESS, FINISHED }
