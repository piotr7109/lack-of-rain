using System;

[Serializable]
public class DialogueOption {
    public string text;
    public int destinationNodeId;
    public NpcReaction reaction;

    public DialogueOption() { }

    public DialogueOption(string text, int dest, NpcReaction reaction = NpcReaction.None) {
        this.text = text;
        this.destinationNodeId = dest;
        this.reaction = reaction;
    }
}
