using System;
using System.Collections.Generic;

[Serializable]
public class DialogueNode {
    public int nodeId = -1;
    public string text;
    public List<DialogueOption> options;

    public DialogueNode() { }

    public DialogueNode(string text) {
        this.text = text;
        this.options = new List<DialogueOption>();
    }
}
