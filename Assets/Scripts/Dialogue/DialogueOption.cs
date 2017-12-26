using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueOption {
    public string text;
    public int destinationNodeId;
    public NpcReaction reaction;
    public NpcReaction reactionTrigger;
    public List<NpcReaction> excludedReactions;
    //It's only one-element List with id of npc quest - by default it's empty
    public List<int> questId;
    public QuestStatus questStatus;

    public DialogueOption() { }

    public DialogueOption(
        string text,
        int dest,
        NpcReaction reaction,
        NpcReaction reactionTrigger,
        List<NpcReaction> excludedReactions,
        List<int> questId,
        QuestStatus questStatus
    ) {
        this.text = text;
        this.destinationNodeId = dest;
        this.reaction = reaction;
        this.reactionTrigger = reactionTrigger;
        this.excludedReactions = excludedReactions;
        this.questId = questId;
        this.questStatus = questStatus;
    }

    public int getQuestId() {
        return questId.Count != 0 ? questId[0] : -1;
    }
}
