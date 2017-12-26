using System.Collections.Generic;
using System;

[Serializable]
public class Dialogue {
    public List<DialogueNode> nodes;

    public Dialogue() {
        nodes = new List<DialogueNode>();
    }

    public void AddNode(DialogueNode node) {
        if (node != null) {
            nodes.Add(node);
            node.nodeId = nodes.IndexOf(node);
        }
    }

    public void AddOption(string text, DialogueNode node, DialogueNode dest) {
        /*if (!nodes.Contains(dest)) {
            AddNode(dest);
        }

        if (!nodes.Contains(node)) {
            AddNode(node);
        }

        int destNodeId = dest != null ? dest.nodeId : -1;
        DialogueOption option = new DialogueOption(text, destNodeId);

        node.options.Add(option);*/
    }
}
