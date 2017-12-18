using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    #region Singleton

    public static DialogueManager instance;

    void Awake() {
        instance = this;
    }

    #endregion

    public GameObject dialogueUI;

    public Text dialogueText;
    public RectTransform optionsPanel;
    public Button optionPrefab;

    private static Dialogue dialogue;
    private GameObject npc;

    public void RunDialogue(string fileName, GameObject npc) {
        this.npc = npc;
        dialogue = DialogueSerialization.LoadDialogue(fileName);
        dialogueUI.SetActive(true);

        RunNode(0, NpcReaction.None);
    }

    void RunNode(int nodeIndex, NpcReaction reaction) {
        CleanOptionsPanel();

        if (nodeIndex == -1) {
            dialogueUI.SetActive(false);
            ExecuteNpcReaction(reaction);
            dialogue = null;
            npc = null;

            return;
        }

        DialogueNode node = dialogue.nodes[nodeIndex];
        dialogueText.text = node.text;

        node.options.ForEach(option => {
            Button optButton = Instantiate(optionPrefab, optionsPanel) as Button;
            optButton.GetComponentInChildren<Text>().text = option.text;
            optButton.onClick.AddListener(delegate { RunNode(option.destinationNodeId, option.reaction); });
        });
    }

    private void CleanOptionsPanel() {
        foreach (Transform child in optionsPanel) {
            Destroy(child.gameObject);
        }
    }

    private void ExecuteNpcReaction(NpcReaction reaction) {
        switch (reaction) {
            case NpcReaction.Attack:
                Debug.Log("ATTACK!");
                break;
        }
    }
}

public enum NpcReaction { None, Attack };